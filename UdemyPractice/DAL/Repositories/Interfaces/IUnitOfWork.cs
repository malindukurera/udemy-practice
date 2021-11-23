using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories
{
    public interface IUnitOfWork
    {
        IDepartmentRepository DepartmentRepository { get; }
        IStudentRepository StudentRepository { get; }
        ICourseRepository CourseRepository { get; }
        ICourseStudentRepository CourseStudentRepository { get; }
        ITransactionHistoryRepository TransactionHistoryRepository { get; }
        ICustomerBalanceRepository CustomerBalanceRepository { get; }
        Task<bool> SaveChangesAsync();
    }
}
