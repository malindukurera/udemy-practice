using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IUnitOfWork
    {
        IDepartmentRepository DepartmentRepository { get; }
        IStudentRepository StudentRepository { get; }
        Task<bool> SaveChangesAsync();
    }
}
