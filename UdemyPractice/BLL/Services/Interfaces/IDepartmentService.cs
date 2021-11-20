using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Request;
using DAL.Model;

namespace BLL.Services
{
    public interface IDepartmentService
    {
        IQueryable<Department> Queryable();
        Task<Department> GetByAsync(string code);
        Task<Department> AddAsync(DepartmentInsertRequestViewModel department);
        Task<Department> UpdateAsync(string code, DepartmentInsertRequestViewModel department);
        Task<Department> DeleteAsync(string code);
        Task<bool> IsCodeExists(string code);
        Task<bool> IsNameExists(string name);
        Task<bool> IsIdExists(int id);
    }
}
