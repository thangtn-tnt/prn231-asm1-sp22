using AutoMapper;
using eStore.Models;
using eStore.Models.Dto;
using eStore.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
        private readonly IProductService _service;
        public HomeController(IProductService service)
        {            
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDTO> listProducts = new List<ProductDTO>();

            var response = await _service.GetAllAsync<APIResponse>();

            if (response != null && response.IsSuccess)
            {
                listProducts = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(response.Result));
            }

            return View(listProducts);
        }
    }
}
