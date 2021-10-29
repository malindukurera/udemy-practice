using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Model;

namespace DAL.Repositories
{
    public interface IDepartmentRepository
    {
        Task<Department> AddAsync(Department department);
        Task<List<Department>> GetAllAsync();
        Task<Department> DeleteAsync(string code);
        Task<Department> GetByAsync(string code);
        Task<Department> UpdateAsync(string code, Department department);
        Task<object> FindByCode(string code);
        Task<object> FindByName(string name);
    }
}