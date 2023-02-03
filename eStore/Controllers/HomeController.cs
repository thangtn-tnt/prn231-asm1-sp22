using AutoMapper;
using eStore.Models;
using eStore.Models.Dto;
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
        private readonly IMapper _mapper;
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient client = null;
        private string ProductApiUrl = string.Empty;
        private readonly JsonSerializerOptions options;
        public HomeController(ILogger<HomeController> logger, IMapper mapper)
        {
            _mapper = mapper;
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

        public async Task<IActionResult> Index()
        {
            List<ProductDTO> listProducts = new List<ProductDTO>();

            var response = await client.GetAsync(ProductApiUrl);

            if (response != null)
            {
                string strData = await response.Content.ReadAsStringAsync();

                listProducts = !string.IsNullOrEmpty(strData) ? JsonSerializer.Deserialize<List<ProductDTO>>(strData, options) : null;
            }

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
