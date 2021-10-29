using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;

namespace BLL.Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllAsync();
        Task<Student> GetByAsync(string code);
        Task<Student> AddAsync(Student student);
        Task<Student> UpdateAsync(string code, Student student);
        Task<Student> DeleteAsync(string code);
    }
}
