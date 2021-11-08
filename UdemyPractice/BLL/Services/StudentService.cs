using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;
using DAL.Repositories;
using Utility.Exceptions;

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
            return await _studentRepository.GetList();
        }

        public async Task<Student> GetByAsync(string email)
        {
            var dbStudent = await _studentRepository.FindSingleAsync(s => s.Email == email);
            if (dbStudent == null)
            {
                throw new ApplicationValidationException("Student not found");
            }

            return dbStudent;
        }

        public async Task<Student> AddAsync(Student student)
        {
            await _studentRepository.CreateAsync(student);

            if (await _studentRepository.SaveCompletedAsync())
            {
                return student;
            }

            throw new ApplicationValidationException("Error inserting student");
        }

        public async Task<Student> UpdateAsync(string email, Student student)
        {
            var dbStudent = await _studentRepository.FindSingleAsync(s => s.Email == email);
            if (dbStudent == null)
            {
                throw new ApplicationValidationException("Student not found");
            }

            dbStudent.Name = student.Name;
            _studentRepository.Update(dbStudent);

            if (await _studentRepository.SaveCompletedAsync())
            {
                return dbStudent;
            }

            throw new ApplicationValidationException("Error updating the student");
        }

        public async Task<Student> DeleteAsync(string email)
        {
            var dbStudent = await _studentRepository.FindSingleAsync(s => s.Email == email);
            if (dbStudent == null)
            {
                throw new ApplicationValidationException("Student not found");
            }

            _studentRepository.Delete(dbStudent);

            if (await _studentRepository.SaveCompletedAsync())
            {
                return dbStudent;
            }

            throw new ApplicationValidationException("Error deleting the student");
        }
    }
}
