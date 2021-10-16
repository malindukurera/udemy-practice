using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        //[HttpGet]
        //public IActionResult Index()
        //{
        //    return Ok("Hi world");
        //}

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("Get all students");
        }

        [HttpGet("{code}")]
        public IActionResult Get(string code)
        {
            return Ok("Get this " + code + " department data");
        }

        [HttpPost]
        public IActionResult Insert()
        {
            return Ok("Insert new department");
        }

        [HttpPut("{code}")]
        public IActionResult Update(string code)
        {
            return Ok("Update this " + code + " department data");
        }

        [HttpDelete("{code}")]
        public IActionResult Delete(string code)
        {
            return Ok("Delete this " + code + " department data");
        }
    }
}
