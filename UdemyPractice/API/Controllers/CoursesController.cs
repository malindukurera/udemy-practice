using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Request;
using BLL.Services;
using DAL.Model;
using DAL.Repositories;

namespace API.Controllers
{
    public class CoursesController : MainApiController
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _courseService.GetAllAsync());
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> Get(string code)
        {
            return Ok(await _courseService.GetByAsync(code));
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] CourseInsertRequestViewModel course)
        {
            return Ok(await _courseService.AddAsync(course));
        }

        [HttpPut("{code}")]
        public async Task<IActionResult> Update(string code, CourseInsertRequestViewModel course)
        {
            return Ok(await _courseService.UpdateAsync(code, course));
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            return Ok(await _courseService.DeleteAsync(code));
        }
    }

    public static class CourseStatic 
    {
        private static List<Course> AllCourses { get; set; } = new List<Course>();

        public static Course Insert(Course course)
        {
            AllCourses.Add(course);
            return course;
        }

        public static List<Course> GetAll()
        {
            return AllCourses;
        }

        public static Course GetByCode(string code)
        {
            return AllCourses.FirstOrDefault(d => d.Code.ToLower() == code.ToLower());
        }

        public static Course Update(string code, Course course)
        {
            Course result = new Course();

            foreach (var aCourse in AllCourses)
            {
                if (aCourse.Code == code)
                {
                    aCourse.Name = course.Name;
                    result = aCourse;
                }
            }

            return result;
        }

        public static Course Delete(string code)
        {
            var dept = AllCourses.FirstOrDefault(d => d.Code.ToLower() == code.ToLower());
            AllCourses = AllCourses.Where(x => x.Code != dept.Code).ToList();
            return dept;
        }
    }
}
