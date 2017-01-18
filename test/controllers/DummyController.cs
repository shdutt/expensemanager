using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test.Entities;

namespace test.controllers
{
    public class DummyController : Controller
    {
        private ExpenseInfo _Exc;
        private Task context;

        public DummyController(ExpenseInfo Exc)
        {
            _Exc = Exc;
        }

        [HttpGet]
        [Route("api/testDB")]
        public IActionResult testDB()
        {
            return Ok();
        }
        [HttpGet]
        [Route("api/check")]
        public void check()
        {
            Console.WriteLine("checking!");
        }
    }
}
