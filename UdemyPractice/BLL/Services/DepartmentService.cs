using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Request;
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

        public async Task<Department> AddAsync(DepartmentInsertRequestViewModel request)
        {
            var department = new Department();
            department.Code = request.Code;
            department.Name = request.Name;

            return await _departmentRepository.AddAsync(department);
        }

        public async Task<Department> UpdateAsync(string code, DepartmentInsertRequestViewModel request)
        {
            var department = new Department();
            department.Code = request.Code;
            department.Name = request.Name;

            return await _departmentRepository.UpdateAsync(code, department);
        }

        public async Task<Department> DeleteAsync(string code)
        {
            return await _departmentRepository.DeleteAsync(code);
        }

        public async Task<bool> IsCodeExists(string code)
        {
            var dept = await _departmentRepository.FindByCode(code);
            if (dept == null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsNameExists(string name)
        {
            var dept = await _departmentRepository.FindByName(name);
            if (dept == null)
            {
                return true;
            }

            return false;
        }
    }
}
