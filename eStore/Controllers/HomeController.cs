using BusinessObject;
using eStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace eStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient client = null;
        private string ProductApiUrl = string.Empty;
        private readonly JsonSerializerOptions options;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:44318/api/Product";

            options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        public async Task<IActionResult> IndexAsync()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);

            string strData = await response.Content.ReadAsStringAsync();

            List<Product> listProducts = !string.IsNullOrEmpty(strData) ? JsonSerializer.Deserialize<List<Product>>(strData, options) : null;

            return View(listProducts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
