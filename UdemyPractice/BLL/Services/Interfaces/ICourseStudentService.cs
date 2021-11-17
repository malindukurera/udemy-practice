using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Request;
using DAL.Model;
using DAL.ResponseViewModel;
using Utility.Models;

namespace BLL.Services.Interfaces
{
    public interface ICourseStudentService
    {
        Task<List<CourseStudent>> GetAllAsync();
        Task<CourseStudent> GetByAsync(int courseId, int studentId);
        Task<ApiSuccessResponse> AddAsync(CourseStudentInsertRequestViewModel courseStudent);
        Task<CourseStudent> DeleteAsync(int courseId, int studentId);
        Task<StudentCourseViewModel> CourseListAsync(int studentId);
    }
}
