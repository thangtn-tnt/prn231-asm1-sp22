using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using eStore.Models.Dto;
using eStore.Services;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using eStore.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace eStore.Areas.Admin.Controllers
{
    [Area("Manage")]
    public class SalesController : Controller
    {
        private readonly IOrderDetailService _service;
        public SalesController(IOrderDetailService service)
        {
            _service = service;
        }
        public async Task<ActionResult> Index(string? startDate, string? endDate)
        {
            //SaleModel input = new SaleModel();
            if (HttpContext.Session.GetString(SD.SessionRole) == "Admin")
            {
                var sales = new List<ProductSalesDTO>();
                if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                {
                    if (startDate.CompareTo(endDate) < 0)
                    {
                        var response = await _service.GetAllAsync<APIResponse>(startDate, endDate);
                        if (response != null && response.IsSuccess)
                        {
                            sales = JsonConvert.DeserializeObject<List<ProductSalesDTO>>(Convert.ToString(response.Result));
                        }
                    }
                }
                return View(sales);
            }

            return Redirect("/");
        }
    }
}
