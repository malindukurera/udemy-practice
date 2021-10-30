using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Request;
using DAL.Model;
using DAL.Repositories;
using Utility.Exceptions;

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
            var dept = await _departmentRepository.GetByAsync(code);
            if (dept == null)
            {
                throw new ApplicationValidationException("department not found");
            }

            return dept;
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
            var dept = await _departmentRepository.GetByAsync(code);
            if (dept == null)
            {
                throw new ApplicationValidationException("department not found");
            }

            if (!String.IsNullOrEmpty(request.Code))
            {
                var existAlreadyCode = await _departmentRepository.FindByCode(request.Code);
                if (existAlreadyCode != null)
                {
                    throw new ApplicationValidationException("Your updated Code already present in our system!");
                }

                dept.Code = request.Code;
            }

            if (!String.IsNullOrEmpty(request.Name))
            {
                var existAlreadyName = await _departmentRepository.FindByName(request.Name);
                if (existAlreadyName != null)
                {
                    throw new ApplicationValidationException("Your updated Name already present in our system!");
                }

                dept.Name = request.Name;
            }

            if (await _departmentRepository.UpdateAsync(dept))
            {
                return dept;
            }

            throw new ApplicationValidationException("Some problem for update data");
        }

        public async Task<Department> DeleteAsync(string code)
        {
            var department = await _departmentRepository.GetByAsync(code);
            if (department == null)
            {
                throw new ApplicationValidationException("department not found");
            }

            if (await _departmentRepository.DeleteAsync(department))
            {
                return department;
            }

            throw new ApplicationValidationException("Some problem for delete data");
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
