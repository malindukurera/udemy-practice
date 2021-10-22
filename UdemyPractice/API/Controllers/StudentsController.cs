using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.Controllers
{
    
    public class StudentsController : MainApiController
    {
        [HttpGet]
        public IActionResult GetAll([FromQuery] string rollNumber, 
            [FromQuery] string nickName)
        {
            return Ok(StudentStatic.GetAll());
        }

        [HttpGet("{code}")]
        public IActionResult Get(string code)
        {
            return Ok(StudentStatic.GetByCode(code));
        }

        [HttpPost]
        public IActionResult Insert([FromForm] Student student)
        {
            return Ok(StudentStatic.Insert(student));
        }

        [HttpPut("{code}")]
        public IActionResult Update(string code, Student student)
        {
            return Ok(StudentStatic.Update(code, student));
        }

        [HttpDelete("{code}")]
        public IActionResult Delete(string code)
        {
            return Ok(StudentStatic.Delete(code));
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
