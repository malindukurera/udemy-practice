using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.DBContext;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool isDisposed = false;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        private IDepartmentRepository _departmentRepository;
        public IDepartmentRepository DepartmentRepository => 
            _departmentRepository ?? new DepartmentRepository(_context);

        public IStudentRepository _studentRepository;
        public IStudentRepository StudentRepository => 
            _studentRepository ?? new StudentRepository(_context);

        private ICourseRepository _courseRepository;
        public ICourseRepository CourseRepository =>
            _courseRepository ?? new CourseRepository(_context);

        private ICourseStudentRepository _courseStudentRepository;
        public ICourseStudentRepository CourseStudentRepository =>
            _courseStudentRepository ?? new CourseStudentRepository(_context);

        private ICustomerBalanceRepository _customerBalanceRepository;
        public ICustomerBalanceRepository CustomerBalanceRepository =>
            _customerBalanceRepository ?? new CustomerBalanceRepository(_context);

        private ITransactionHistoryRepository _transactionHistoryRepository;
        public ITransactionHistoryRepository TransactionHistoryRepository =>
            _transactionHistoryRepository ?? new TransactionHistoryRepository(_context);

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            isDisposed = true;
        }
    }
}
