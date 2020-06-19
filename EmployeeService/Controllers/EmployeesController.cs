using System.Collections.Generic;
using EmployeeService.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        // GET api/employees
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetAllEmployees()
        {
            var commandItems = new List<Employee>
            {
                new Employee{FirstName="Jack",LastName="Ryan",PhoneNumber="760-000-0000"},
                new Employee{FirstName="Jason",LastName="Borne",PhoneNumber="760-000-0001"},
                new Employee{FirstName="James",LastName="Bond",PhoneNumber="760-000-0010"}
            };

            return Ok(commandItems);
        }

        // GET api/employees/5
        [HttpGet("{id}", Name = "GetEmployeeById")]
        public ActionResult<Employee> GetEmployeeById(int id)
        {
            return Ok(new Employee { FirstName = "Jack", LastName = "Ryan", PhoneNumber = "760-000-0000" });
        }

        // POST api/employees
        [HttpPost]
        public ActionResult<Employee> CreateEmployee(Employee emp)
        {
            return CreatedAtRoute(nameof(GetEmployeeById), new { Id = emp.Id }, emp);
        }
    }
}