using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Request;
using DAL.Model;

namespace BLL.Services
{
    public interface ICourseService
    {
        Task<List<Course>> GetAllAsync();
        Task<Course> GetByAsync(string code);
        Task<Course> AddAsync(CourseInsertRequestViewModel course);
        Task<Course> UpdateAsync(string code, CourseInsertRequestViewModel course);
        Task<Course> DeleteAsync(string code);
        Task<bool> IsCodeExists(string code);
        Task<bool> IsNameExists(string name);
    }
}
