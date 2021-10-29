using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.DBContext;
using DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        public async Task<Department> AddAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> DeleteAsync(string code)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(x => x.Code == code);
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<Department> GetByAsync(string code)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(x => x.Code == code);
            return department;
        }

        public async Task<Department> UpdateAsync(string code, Department department)
        {
            var dept = await _context.Departments.FirstOrDefaultAsync(x => x.Code == code);

            dept.Name = department.Name;
            _context.Departments.Update(dept);
            await _context.SaveChangesAsync();

            return department;
        }

        public async Task<object> FindByCode(string code)
        {
            return await _context.Departments.FirstOrDefaultAsync(x => x.Code == code);
        }

        public async Task<object> FindByName(string name)
        {
            return await _context.Departments.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}