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
        private readonly IUnitOfWork _uow;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await _uow.DepartmentRepository.GetList();
        }

        public async Task<Department> GetByAsync(string code)
        {
            var dept = await _uow.DepartmentRepository.FindSingleAsync(x => x.Code == code);
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

            await _uow.DepartmentRepository.CreateAsync(department);

            if (await _uow.DepartmentRepository.SaveCompletedAsync())
            {
                return department;
            }

            throw new ApplicationValidationException("Error inserting department");
        }

        public async Task<Department> UpdateAsync(string code, DepartmentInsertRequestViewModel request)
        {
            var dept = await _uow.DepartmentRepository.FindSingleAsync(x => x.Code == code);
            if (dept == null)
            {
                throw new ApplicationValidationException("department not found");
            }

            if (!String.IsNullOrEmpty(request.Code))
            {
                var existAlreadyCode = await _uow.DepartmentRepository.FindSingleAsync(x => x.Code == request.Code);
                if (existAlreadyCode != null)
                {
                    throw new ApplicationValidationException("Your updated Code already present in our system!");
                }

                dept.Code = request.Code;
            }

            if (!String.IsNullOrEmpty(request.Name))
            {
                var existAlreadyName = await _uow.DepartmentRepository.FindSingleAsync(x => x.Name == request.Name);
                if (existAlreadyName != null)
                {
                    throw new ApplicationValidationException("Your updated Name already present in our system!");
                }

                dept.Name = request.Name;
            }

            _uow.DepartmentRepository.Update(dept);
            if (await _uow.DepartmentRepository.SaveCompletedAsync())
            {
                return dept;
            }

            throw new ApplicationValidationException("Some problem for update data");
        }

        public async Task<Department> DeleteAsync(string code)
        {
            var department = await _uow.DepartmentRepository.FindSingleAsync(x=>x.Code == code);
            if (department == null)
            {
                throw new ApplicationValidationException("department not found");
            }

            _uow.DepartmentRepository.Delete(department);

            if (await _uow.DepartmentRepository.SaveCompletedAsync())
            {
                return department;
            }

            throw new ApplicationValidationException("Some problem for delete data");
        }

        public async Task<bool> IsCodeExists(string code)
        {
            var dept = await _uow.DepartmentRepository.FindSingleAsync(x => x.Code == code);
            if (dept == null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsNameExists(string name)
        {
            var dept = await _uow.DepartmentRepository.FindSingleAsync(x => x.Name == name);
            if (dept == null)
            {
                return true;
            }

            return false;
        }
    }
}
