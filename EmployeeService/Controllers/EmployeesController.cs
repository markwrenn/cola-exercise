using System.Collections.Generic;
using EmployeeService.Data;
using EmployeeService.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private static IData db = new MockDatabase();

        // GET api/employees
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetAllEmployees()
        {
            return Ok(db.GetAll());
        }

        // GET api/employees/5
        [HttpGet("{id}", Name = "GetEmployeeById")]
        public ActionResult<Employee> GetEmployeeById(int id)
        {
            var emp = db.GetById(id);
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
            return Ok(db.Add(emp));
        }

        // DELETE api/employees/5
        [HttpDelete("{id}")]
        public ActionResult DeleteEmployee(int id)
        {
            if (db.Delete(id))
            {
                return Ok();
            }

            return NotFound();
        }
    }
}