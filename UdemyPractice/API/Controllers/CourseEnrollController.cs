using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Request;
using BLL.Services.Interfaces;

namespace API.Controllers
{
    public class CourseEnrollController : Controller
    {
        private readonly ICourseStudentService _courseStudentService;

        public CourseEnrollController(ICourseStudentService courseStudentService)
        {
            _courseStudentService = courseStudentService;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(CourseStudentInsertRequestViewModel model)
        {
            return Ok(await _courseStudentService.AddAsync(model));
        }

        [HttpGet("{studentId}")]
        public async Task<IActionResult> CourseList(int studentId)
        {
            return Ok(await _courseStudentService.CourseListAsync(studentId));
        }
    }
}
