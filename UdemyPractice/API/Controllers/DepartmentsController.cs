using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Request;
using BLL.Services;
using DAL.Model;
using DAL.Repositories;
using LightQuery.EntityFrameworkCore;

namespace API.Controllers
{
    public class DepartmentsController : MainApiController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [AsyncLightQuery(forcePagination: true, defaultPageSize: 10, defaultSort: "Id desc")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_departmentService.Queryable());
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> Get(string code)
        {
            return Ok(await _departmentService.GetByAsync(code));
        }

        [HttpPost]
        public async Task<IActionResult> Insert(DepartmentInsertRequestViewModel department)
        {
            return Ok(await _departmentService.AddAsync(department));
        }

        [HttpPut("{code}")]
        public async Task<IActionResult> Update(string code, DepartmentInsertRequestViewModel department)
        {
            return Ok(await _departmentService.UpdateAsync(code, department));
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            return Ok(await _departmentService.DeleteAsync(code));
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
