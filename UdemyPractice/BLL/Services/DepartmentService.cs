using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;
using DAL.Repositories;

namespace BLL.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await _departmentRepository.GetAllAsync();
        }

        public async Task<Department> GetByAsync(string code)
        {
            return await _departmentRepository.GetByAsync(code);
        }

        public async Task<Department> AddAsync(Department department)
        {
            return await _departmentRepository.AddAsync(department);
        }

        public async Task<Department> UpdateAsync(string code, Department department)
        {
            return await _departmentRepository.UpdateAsync(code, department);
        }

        public async Task<Department> DeleteAsync(string code)
        {
            return await _departmentRepository.DeleteAsync(code);
        }
    }
}
