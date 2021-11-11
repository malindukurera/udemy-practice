using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Request;
using DAL.Model;

namespace BLL.Services.Interfaces
{
    public interface ICourseStudentService
    {
        Task<List<CourseStudent>> GetAllAsync();
        Task<CourseStudent> GetByAsync(string code);
        Task<CourseStudent> AddAsync(CourseStudentInsertRequestViewModel courseStudent);
        Task<CourseStudent> UpdateAsync(string code, CourseStudentInsertRequestViewModel courseStudent);
        Task<CourseStudent> DeleteAsync(string code);
        Task<bool> IsCodeExists(string code);
        Task<bool> IsNameExists(string name);
    }
}
