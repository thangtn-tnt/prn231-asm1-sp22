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
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Data;

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

        public async Task<IActionResult> Index(string? search)
        {
            if (HttpContext.Session.GetString(SD.SessionRole) == "Admin")
            {
                List<ProductDTO> listProducts = new List<ProductDTO>();

                var response = await _service.GetAllAsync<APIResponse>(search);

                if (response != null && response.IsSuccess)
                {
                    listProducts = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(response.Result));
                }

                return View(listProducts);
            }
            return Redirect("/");
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            if (HttpContext.Session.GetString(SD.SessionRole) == "Admin")
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
            return Redirect("/");
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
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Create");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (HttpContext.Session.GetString(SD.SessionRole) == "Admin")
            {
                var response = await _service.GetAsync<APIResponse>(id);
                if (response != null && response.IsSuccess)
                {
                    ProductDTO model = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
                    return View(model);
                }
                return NotFound();
            }
            return Redirect("/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ProductDTO model)
        {
            var response = await _service.DeleteAsync<APIResponse>(model.ProductId);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            if (HttpContext.Session.GetString(SD.SessionRole) == "Admin")
            {
                List<CategoryDTO> categories = new List<CategoryDTO>();

                var responseCate = await _service.GetForeignKeyList<APIResponse>();

                if (responseCate != null && responseCate.IsSuccess)
                {
                    categories = JsonConvert.DeserializeObject<List<CategoryDTO>>(Convert.ToString(responseCate.Result));
                }


                var response = await _service.GetAsync<APIResponse>(id);
                if (response != null && response.IsSuccess)
                {
                    ProductDTO tmp = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));

                    ProductUpdateDTO product = _mapper.Map<ProductUpdateDTO>(tmp);

                    product.CategoryId = categories.SingleOrDefault(c => c.CategoryName == tmp.CategoryName).CategoryId;

                    ViewData["CategoryName"] = new SelectList(categories, "CategoryId", "CategoryName");

                    return View(product);
                }             
                return NotFound();
            }
            return Redirect("/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ProductUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _service.UpdateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Update));
        }


    }
}

