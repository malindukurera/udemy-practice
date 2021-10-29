using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;

namespace DAL.Repositories
{
    public interface IStudentRepository
    {
        Task<Student> AddAsync(Student student);
        Task<List<Student>> GetAllAsync();
        Task<Student> DeleteAsync(string email);
        Task<Student> GetByAsync(string email);
        Task<Student> UpdateAsync(string email, Student student);
    }
}
