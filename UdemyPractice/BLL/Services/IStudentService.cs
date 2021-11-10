using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Request;
using DAL.Model;

namespace BLL.Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllAsync();
        Task<Student> GetByAsync(string email);
        Task<Student> AddAsync(StudentInsertRequestViewModel studentRequest);
        Task<Student> UpdateAsync(string email, Student student);
        Task<Student> DeleteAsync(string email);
        Task<bool> IsEmailExists(string email);
    }
}
