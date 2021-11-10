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
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _uow;

        public StudentService(IUnitOfWork _uow)
        {
            this._uow = _uow;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _uow.StudentRepository.GetList();
        }

        public async Task<Student> GetByAsync(string email)
        {
            var dbStudent = await _uow.StudentRepository.FindSingleAsync(s => s.Email == email);
            if (dbStudent == null)
            {
                throw new ApplicationValidationException("Student not found");
            }

            return dbStudent;
        }

        public async Task<Student> AddAsync(StudentInsertRequestViewModel studentRequest)
        {
            var student = new Student();
            student.Name = studentRequest.Name;
            student.Email = studentRequest.Email;
            student.DepartmentId = studentRequest.DepartmentId;

            await _uow.StudentRepository.CreateAsync(student);
            if (await _uow.StudentRepository.SaveCompletedAsync())
            {
                return student;
            }

            throw new ApplicationValidationException("Error inserting student");
        }

        public async Task<Student> UpdateAsync(string email, Student student)
        {
            var dbStudent = await _uow.StudentRepository.FindSingleAsync(s => s.Email == email);
            if (dbStudent == null)
            {
                throw new ApplicationValidationException("Student not found");
            }

            dbStudent.Name = student.Name;
            _uow.StudentRepository.Update(dbStudent);

            if (await _uow.StudentRepository.SaveCompletedAsync())
            {
                return dbStudent;
            }

            throw new ApplicationValidationException("Error updating the student");
        }

        public async Task<Student> DeleteAsync(string email)
        {
            var dbStudent = await _uow.StudentRepository.FindSingleAsync(s => s.Email == email);
            if (dbStudent == null)
            {
                throw new ApplicationValidationException("Student not found");
            }

            _uow.StudentRepository.Delete(dbStudent);

            if (await _uow.StudentRepository.SaveCompletedAsync())
            {
                return dbStudent;
            }

            throw new ApplicationValidationException("Error deleting the student");
        }

        public async Task<bool> IsEmailExists(string email)
        {
            var stu = await _uow.StudentRepository.FindSingleAsync(s => s.Email == email);
            if (stu == null)
                return true;
            return false;
        }
    }
}
