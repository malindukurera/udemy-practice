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
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _uow;

        public CourseService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        public async Task<List<Course>> GetAllAsync()
        {
            return await _uow.CourseRepository.GetList();
        }

        public async Task<Course> GetByAsync(string code)
        {
            var dept = await _uow.CourseRepository.FindSingleAsync(x => x.Code == code);
            if (dept == null)
            {
                throw new ApplicationValidationException("course not found");
            }

            return dept;
        }

        public async Task<Course> AddAsync(CourseInsertRequestViewModel request)
        {
            var course = new Course();
            course.Code = request.Code;
            course.Name = request.Name;

            await _uow.CourseRepository.CreateAsync(course);

            if (await _uow.CourseRepository.SaveCompletedAsync())
            {
                return course;
            }

            throw new ApplicationValidationException("Error inserting course");
        }

        public async Task<Course> UpdateAsync(string code, CourseInsertRequestViewModel request)
        {
            var dept = await _uow.CourseRepository.FindSingleAsync(x => x.Code == code);
            if (dept == null)
            {
                throw new ApplicationValidationException("course not found");
            }

            if (!String.IsNullOrEmpty(request.Code))
            {
                var existAlreadyCode = await _uow.CourseRepository.FindSingleAsync(x => x.Code == request.Code);
                if (existAlreadyCode != null)
                {
                    throw new ApplicationValidationException("Your updated Code already present in our system!");
                }

                dept.Code = request.Code;
            }

            if (!String.IsNullOrEmpty(request.Name))
            {
                var existAlreadyName = await _uow.CourseRepository.FindSingleAsync(x => x.Name == request.Name);
                if (existAlreadyName != null)
                {
                    throw new ApplicationValidationException("Your updated Name already present in our system!");
                }

                dept.Name = request.Name;
            }

            _uow.CourseRepository.Update(dept);
            if (await _uow.CourseRepository.SaveCompletedAsync())
            {
                return dept;
            }

            throw new ApplicationValidationException("Some problem for update data");
        }

        public async Task<Course> DeleteAsync(string code)
        {
            var course = await _uow.CourseRepository.FindSingleAsync(x=>x.Code == code);
            if (course == null)
            {
                throw new ApplicationValidationException("course not found");
            }

            _uow.CourseRepository.Delete(course);

            if (await _uow.CourseRepository.SaveCompletedAsync())
            {
                return course;
            }

            throw new ApplicationValidationException("Some problem for delete data");
        }

        public async Task<bool> IsCodeExists(string code)
        {
            var dept = await _uow.CourseRepository.FindSingleAsync(x => x.Code == code);
            if (dept == null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsNameExists(string name)
        {
            var dept = await _uow.CourseRepository.FindSingleAsync(x => x.Name == name);
            if (dept == null)
            {
                return true;
            }

            return false;
        }
    }
}
