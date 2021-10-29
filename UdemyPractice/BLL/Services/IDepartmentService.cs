using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;

namespace BLL.Services
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetAllAsync();
        Task<Department> GetByAsync(string code);
        Task<Department> AddAsync(Department department);
        Task<Department> UpdateAsync(string code, Department department);
        Task<Department> DeleteAsync(string code);
    }
}
