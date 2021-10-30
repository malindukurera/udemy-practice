using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Model;

namespace DAL.Repositories
{
    public interface IDepartmentRepository
    {
        Task<Department> AddAsync(Department department);
        Task<List<Department>> GetAllAsync();
        Task<bool> DeleteAsync(Department department);
        Task<Department> GetByAsync(string code);
        Task<bool> UpdateAsync(Department department);
        Task<Department> FindByCode(string code);
        Task<Department> FindByName(string name);
    }
}