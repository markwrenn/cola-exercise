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

namespace EmployeeWeb.Controllers
{

    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public HomeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IEnumerable<Employee> AllEmployees { get; private set; }

        public async Task<IActionResult> IndexAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                        "http://localhost/api/employees");

            request.Headers.Add("Accept", "application/json; charset=utf-8");
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

        public async Task<IActionResult> Add(Employee model)
        {
            if (ModelState.IsValid)
            {
                var request = new HttpRequestMessage(HttpMethod.Post,
                            "http://localhost/api/employees");

                request.Headers.Add("Accept", "application/json; charset=utf-8");
                request.Headers.Add("User-Agent", "EmployeeWeb");

                string jsonstring = JsonSerializer.Serialize<Employee>(model);
                request.Content = new StringContent(jsonstring, Encoding.UTF8, "application/json");

                var client = _clientFactory.CreateClient();
                await client.SendAsync(request);
            }

            return(Redirect("/Home/Index"));
        }

        public async Task<IActionResult> Delete(int id)
        {
            string uri = "http://localhost/api/employees/" + id;

            var request = new HttpRequestMessage(HttpMethod.Delete, uri);

            request.Headers.Add("Accept", "application/json; charset=utf-8");
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
