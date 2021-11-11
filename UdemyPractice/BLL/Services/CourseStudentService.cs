using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Request;
using BLL.Services.Interfaces;
using DAL.Model;
using DAL.Repositories;
using Utility.Exceptions;

namespace BLL.Services
{
    public class CourseStudentService : ICourseStudentService
    {
        private readonly IUnitOfWork _uow;

        public CourseStudentService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        public async Task<List<CourseStudent>> GetAllAsync()
        {
            return await _uow.CourseStudentRepository.GetList();
        }

        public async Task<CourseStudent> GetByAsync(string code)
        {
            var dept = await _uow.CourseStudentRepository.FindSingleAsync(x => x.Code == code);
            if (dept == null)
            {
                throw new ApplicationValidationException("courseStudent not found");
            }

            return dept;
        }

        public async Task<CourseStudent> AddAsync(CourseStudentInsertRequestViewModel request)
        {
            var courseStudent = new CourseStudent();
            courseStudent.Code = request.Code;
            courseStudent.Name = request.Name;

            await _uow.CourseStudentRepository.CreateAsync(courseStudent);

            if (await _uow.CourseStudentRepository.SaveCompletedAsync())
            {
                return courseStudent;
            }

            throw new ApplicationValidationException("Error inserting courseStudent");
        }

        public async Task<CourseStudent> UpdateAsync(string code, CourseStudentInsertRequestViewModel request)
        {
            var dept = await _uow.CourseStudentRepository.FindSingleAsync(x => x.Code == code);
            if (dept == null)
            {
                throw new ApplicationValidationException("courseStudent not found");
            }

            if (!String.IsNullOrEmpty(request.Code))
            {
                var existAlreadyCode = await _uow.CourseStudentRepository.FindSingleAsync(x => x.Code == request.Code);
                if (existAlreadyCode != null)
                {
                    throw new ApplicationValidationException("Your updated Code already present in our system!");
                }

                dept.Code = request.Code;
            }

            if (!String.IsNullOrEmpty(request.Name))
            {
                var existAlreadyName = await _uow.CourseStudentRepository.FindSingleAsync(x => x.Name == request.Name);
                if (existAlreadyName != null)
                {
                    throw new ApplicationValidationException("Your updated Name already present in our system!");
                }

                dept.Name = request.Name;
            }

            _uow.CourseStudentRepository.Update(dept);
            if (await _uow.CourseStudentRepository.SaveCompletedAsync())
            {
                return dept;
            }

            throw new ApplicationValidationException("Some problem for update data");
        }

        public async Task<CourseStudent> DeleteAsync(string code)
        {
            var courseStudent = await _uow.CourseStudentRepository.FindSingleAsync(x => x.Code == code);
            if (courseStudent == null)
            {
                throw new ApplicationValidationException("courseStudent not found");
            }

            _uow.CourseStudentRepository.Delete(courseStudent);

            if (await _uow.CourseStudentRepository.SaveCompletedAsync())
            {
                return courseStudent;
            }

            throw new ApplicationValidationException("Some problem for delete data");
        }

        public async Task<bool> IsCodeExists(string code)
        {
            var dept = await _uow.CourseStudentRepository.FindSingleAsync(x => x.Code == code);
            if (dept == null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsNameExists(string name)
        {
            var dept = await _uow.CourseStudentRepository.FindSingleAsync(x => x.Name == name);
            if (dept == null)
            {
                return true;
            }

            return false;
        }
    }
}
