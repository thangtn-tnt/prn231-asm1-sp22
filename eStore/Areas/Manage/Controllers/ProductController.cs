using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessObject;
using eStore.Services;
using Newtonsoft.Json;
using System;
using eStore.Models.Dto;
using eStore.Models;
using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eStore.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;
        public ProductController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
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

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            List<CategoryDTO> categories = new List<CategoryDTO>();

            var response = await _service.GetForeignKeyList<APIResponse>();

            if (response != null && response.IsSuccess)
            {
                categories = JsonConvert.DeserializeObject<List<CategoryDTO>>(Convert.ToString(response.Result));
            }

            ViewData["CategoryName"] = new SelectList(categories, "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductCreateDTO productCreateDTO)
        {
            if (ModelState.IsValid)
            {
                var response = await _service.CreateAsync<APIResponse>(productCreateDTO);
                if (response != null && response.IsSuccess)
                {
                    ModelState.AddModelError("232", "Error");
                    return RedirectToAction("Index");
                }

            }            
            return RedirectToAction("Create");
        }
    }
}

