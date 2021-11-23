using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Services;

namespace API.Controllers
{
    public class TestController : MainApiController
    {
        private readonly ITestService _service;

        public TestController(ITestService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await _service.DummyData1();
            await _service.DummyData2();
            await _service.AddNewRoles();
            await _service.AddNewUser();

            return Ok("Hello world");
        }
    }
}
