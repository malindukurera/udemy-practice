using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Request;
using BLL.Services.Interfaces;
using DAL.Model;
using DAL.Repositories;
using Utility.Exceptions;
using Utility.Models;

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

        public async Task<CourseStudent> GetByAsync(int courseId, int studentId)
        {
            var dept = await _uow.CourseStudentRepository
                .FindSingleAsync(x => x.CourseId == courseId && x.StudentId == studentId);
            if (dept == null)
            {
                throw new ApplicationValidationException("courseStudent not found");
            }

            return dept;
        }

        public async Task<ApiSuccessResponse> AddAsync(CourseStudentInsertRequestViewModel request)
        {
            var dept = await _uow.CourseStudentRepository
                .FindSingleAsync(x => x.CourseId == request.CourseId && x.StudentId == request.StudentId);
            if (dept != null)
            {
                throw new ApplicationValidationException("Student already enrolled to this course");
            }

            var courseStudent = new CourseStudent();
            courseStudent.CourseId = request.CourseId;
            courseStudent.StudentId = request.StudentId;

            await _uow.CourseStudentRepository.CreateAsync(courseStudent);

            if (await _uow.CourseStudentRepository.SaveCompletedAsync())
            {
                return new ApiSuccessResponse()
                {
                    Message = "Student enrolled successfully",
                    StatusCode = 200
                };
            }

            throw new ApplicationValidationException("Error inserting courseStudent");
        }

        public async Task<CourseStudent> DeleteAsync(int courseId, int studentId)
        {
            var dept = await _uow.CourseStudentRepository
                .FindSingleAsync(x => x.CourseId == courseId && x.StudentId == studentId);
            if (dept == null)
            {
                throw new ApplicationValidationException("courseStudent not found");
            }

            _uow.CourseStudentRepository.Delete(dept);

            if (await _uow.CourseStudentRepository.SaveCompletedAsync())
            {
                return dept;
            }

            throw new ApplicationValidationException("Some problem for delete data");
        }
    }
}
