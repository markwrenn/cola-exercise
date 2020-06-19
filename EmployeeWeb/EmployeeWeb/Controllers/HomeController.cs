using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeWeb.Models;

namespace EmployeeWeb.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static Dictionary<int, Employee> _empMap = new Dictionary<int, Employee>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            if (_empMap.Count == 0)
            {
                _empMap[1] = new Employee { Id = 1, FirstName = "Jim", LastName = "Smith", PhoneNumber = "760-000-0001" };
                _empMap[2] = new Employee { Id = 2, FirstName = "Aaron", LastName = "Bentley", PhoneNumber = "760-000-0002" };
                _empMap[3] = new Employee { Id = 3, FirstName = "Sarah", LastName = "Francis", PhoneNumber = "760-000-0003" };
            }
        }

        public IActionResult Index()
        {
            List<Employee> emplist = new List<Employee>();

            foreach (var item in _empMap) {
                emplist.Add(item.Value);
            }

            return View(emplist);
        }

        public IActionResult Add(Employee model)
        {
            if (ModelState.IsValid)
            {
                int newId = 0;
                foreach (var item in _empMap)
                {
                    newId = newId < item.Value.Id ? item.Value.Id : newId;
                }
                model.Id = newId + 1;
                _empMap.Add(model.Id, model);
            }

            return Redirect("/Home/Index");
        }

        public IActionResult Delete(int id)
        {
            if (_empMap.ContainsKey(id))
            {
                _empMap.Remove(id);
            }

            return Redirect("/Home/Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
