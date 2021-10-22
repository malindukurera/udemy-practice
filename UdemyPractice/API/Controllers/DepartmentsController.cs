using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.Controllers
{
    public class DepartmentsController : MainApiController
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(DepartmentStatic.GetAll());
        }

        [HttpGet("{code}")]
        public IActionResult Get(string code)
        {
            return Ok(DepartmentStatic.GetByCode(code));
        }

        [HttpPost]
        public IActionResult Insert(Department department)
        {
            return Ok(DepartmentStatic.Insert(department));
        }

        [HttpPut("{code}")]
        public IActionResult Update(string code, Department department)
        {
            return Ok(DepartmentStatic.Update(code, department));
        }

        [HttpDelete("{code}")]
        public IActionResult Delete(string code)
        {
            return Ok(DepartmentStatic.Delete(code));
        }
    }

    public static class DepartmentStatic 
    {
        private static List<Department> AllDepartments { get; set; } = new List<Department>();

        public static Department Insert(Department department)
        {
            AllDepartments.Add(department);
            return department;
        }

        public static List<Department> GetAll()
        {
            return AllDepartments;
        }

        public static Department GetByCode(string code)
        {
            return AllDepartments.FirstOrDefault(d => d.Code.ToLower() == code.ToLower());
        }

        public static Department Update(string code, Department department)
        {
            Department result = new Department();

            foreach (var aDepartment in AllDepartments)
            {
                if (aDepartment.Code == code)
                {
                    aDepartment.Name = department.Name;
                    result = aDepartment;
                }
            }

            return result;
        }

        public static Department Delete(string code)
        {
            var dept = AllDepartments.FirstOrDefault(d => d.Code.ToLower() == code.ToLower());
            AllDepartments = AllDepartments.Where(x => x.Code != dept.Code).ToList();
            return dept;
        }
    }
}
