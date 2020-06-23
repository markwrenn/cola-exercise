using System;
using System.Collections.Generic;
using EmployeeService.Data;
using EmployeeService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EmployeeService.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        // private static IData db = new MockDatabase();
        private static IData _db;

        public EmployeesController(IConfiguration configuration)
        {
            _configuration = configuration;
            string connStr = _configuration["ConnectionStrings:Oracle"];
            _db = new OracleDatabase(connStr);
        }

        // GET api/employees
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetAllEmployees()
        {
            return Ok(_db.GetAll());
        }

        // GET api/employees/5
        [HttpGet("{id}", Name = "GetEmployeeById")]
        public ActionResult<Employee> GetEmployeeById(int id)
        {
            var emp = _db.GetById(id);
            if (emp.Id == 0)
            {
                return NotFound();
            }
            return Ok(emp);
        }

        // POST api/employees
        [HttpPost]
        public ActionResult<Employee> CreateEmployee(Employee emp)
        {
            return Ok(_db.Add(emp));
        }

        // DELETE api/employees/5
        [HttpDelete("{id}")]
        public ActionResult DeleteEmployee(int id)
        {
            if (_db.Delete(id))
            {
                return Ok();
            }

            return NotFound();
        }
    }
}