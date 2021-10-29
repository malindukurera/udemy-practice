using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;
using DAL.Repositories;

namespace BLL.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _studentRepository.GetAllAsync();
        }

        public async Task<Student> GetByAsync(string code)
        {
            return await _studentRepository.GetByAsync(code);
        }

        public async Task<Student> AddAsync(Student student)
        {
            return await _studentRepository.AddAsync(student);
        }

        public async Task<Student> UpdateAsync(string code, Student student)
        {
            return await _studentRepository.UpdateAsync(code, student);
        }

        public async Task<Student> DeleteAsync(string code)
        {
            return await _studentRepository.DeleteAsync(code);
        }
    }
}
