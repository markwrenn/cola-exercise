using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeWeb.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Text.Json;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging.Abstractions;

namespace EmployeeWeb.Controllers
{

    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private static string _phoneRegex = null;

        public HomeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            if (_phoneRegex == null )
            {
                RetrieveValidationExpressions();
            }
        }

        public IEnumerable<Employee> AllEmployees { get; private set; }

        public async Task<IActionResult> IndexAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                        "http://localhost/api/employees");

            request.Headers.Add("User-Agent", "EmployeeWeb");

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                AllEmployees = await JsonSerializer.DeserializeAsync
                    <IEnumerable<Employee>>(responseStream);
            }
            else
            {
                AllEmployees = Array.Empty<Employee>();
            }

            return View(AllEmployees);
        }

        public async void RetrieveValidationExpressions()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                        "http://localhost/api/validations/phonenumber");

            request.Headers.Add("User-Agent", "EmployeeWeb");

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                _phoneRegex = await response.Content.ReadAsStringAsync();
            }
        }

        public JsonResult ValidatePhone(string phoneNumber)
        {
            bool isMatch = Regex.IsMatch(phoneNumber, _phoneRegex);
            return Json(isMatch);
        }


        public async Task<IActionResult> Add(Employee model)
        {
            if(HttpContext.Request.Query["firstpass"].Count == 0 )
            {
                if (ModelState.IsValid)
                {
                    var request = new HttpRequestMessage(HttpMethod.Post,
                                "http://localhost/api/employees");

                    request.Headers.Add("User-Agent", "EmployeeWeb");

                    string jsonstring = JsonSerializer.Serialize<Employee>(model);
                    request.Content = new StringContent(jsonstring, Encoding.UTF8, "application/json");

                    var client = _clientFactory.CreateClient();
                    await client.SendAsync(request);
                    return Redirect("/Home/Index");
                }
            } else
            {
                // First time through skip any validation errors
                ModelState.ClearValidationState("firstName");
                ModelState.ClearValidationState("lastname");
                ModelState.ClearValidationState("phoneNumber");
            }

            return (View());
        }


        public async Task<IActionResult> Delete(int id)
        {
            string uri = "http://localhost/api/employees/" + id;

            var request = new HttpRequestMessage(HttpMethod.Delete, uri);

            request.Headers.Add("User-Agent", "EmployeeWeb");

            var client = _clientFactory.CreateClient();
            await client.SendAsync(request);

            return Redirect("/Home/Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
