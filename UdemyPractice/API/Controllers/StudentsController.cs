using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Model;
using DAL.Repositories;
using BLL.Services;

namespace API.Controllers
{
    
    public class StudentsController : MainApiController
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string rollNumber, 
            [FromQuery] string nickName)
        {
            return Ok(await _studentService.GetAllAsync());
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> Get(string code)
        {
            return Ok(await _studentService.GetByAsync(code));
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] Student student)
        {
            return Ok(await _studentService.AddAsync(student));
        }

        [HttpPut("{code}")]
        public async Task<IActionResult> Update(string code, Student student)
        {
            return Ok(await _studentService.UpdateAsync(code, student));
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            return Ok(await _studentService.DeleteAsync(code));
        }
    }


    public static class StudentStatic
    {
        private static List<Student> AllStudents { get; set; } = new List<Student>();

        public static Student Insert(Student Student)
        {
            AllStudents.Add(Student);
            return Student;
        }

        public static List<Student> GetAll()
        {
            return AllStudents;
        }

        public static Student GetByCode(string email)
        {
            return AllStudents.FirstOrDefault(d => d.Email.ToLower() == email.ToLower());
        }

        public static Student Update(string email, Student student)
        {
            Student result = new Student();

            foreach (var aStudent in AllStudents)
            {
                if (aStudent.Email == email)
                {
                    aStudent.Name = student.Name;
                    result = aStudent;
                }
            }

            return result;
        }

        public static Student Delete(string email)
        {
            var stud = AllStudents.FirstOrDefault(d => d.Email.ToLower() == email.ToLower());
            AllStudents = AllStudents.Where(x => x.Email != stud.Email).ToList();
            return stud;
        }
    }
}
