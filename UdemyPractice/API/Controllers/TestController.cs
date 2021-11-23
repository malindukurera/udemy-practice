using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Services;
using BLL.Services.Interfaces;

namespace API.Controllers
{
    public class TestController : MainApiController
    {
        private readonly ITestService _service;
        private readonly ITransactionService _transactionService;

        public TestController(ITestService service, ITransactionService transactionService)
        {
            _service = service;
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //await _service.DummyData1();
            //await _service.DummyData2();
            //await _service.AddNewRoles();
            //await _service.AddNewUser();

            await _transactionService.InitialTransaction();

            return Ok("Hello world");
        }
    }
}
