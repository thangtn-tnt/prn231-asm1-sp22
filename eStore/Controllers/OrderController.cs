using eStore.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using eStore.Services;
using eStore.Models;
using System.Threading.Tasks;
using AutoMapper;

namespace eStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IProductService _product;
        private readonly IOrderService _order;
        private readonly IOrderDetailService _orderDetail;
        private readonly IMapper _mapper;

        public OrderController(IProductService product, IOrderService order, IOrderDetailService orderDetail, IMapper mapper)
        {
            _product = product;
            _order = order;
            _orderDetail = orderDetail;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Details(int id)
        {
            ProductDTO model = null;

            var productRes = await _product.GetAsync<APIResponse>(id);
            if (productRes != null && productRes.IsSuccess)
            {
                model = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(productRes.Result));

                OrderDetailDTO orderDetail = _mapper.Map<OrderDetailDTO>(model);

                if (orderDetail != null)
                {
                    orderDetail.TempPrice = orderDetail.UnitPrice * orderDetail.Quantity * (1 - orderDetail.Discount / 100);
                    return View(orderDetail);
                }
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult OrderConfirmation()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderConfirmation(OrderDetailDTO orderDetail)
        {
            if (ModelState.IsValid)
            {
                var orderCreate = _mapper.Map<OrderCreateDTO>(orderDetail);
                orderCreate.MemberId = 1;

                var orderRes = await _order.CreateAsync<APIResponse>(orderCreate);

                if (orderRes != null && orderRes.IsSuccess)
                {
                    OrderResponseDTO orderResponse = JsonConvert.DeserializeObject<OrderResponseDTO>(Convert.ToString(orderRes.Result));

                    return View(orderResponse);
                }
                TempData["available"] = "Not available";
            }
            return RedirectToAction("Details", new { id = orderDetail.ProductId });
        }

        [HttpGet]
        public async Task<IActionResult> History()
        {
            List<ProductSalesDTO> listOrders = new List<ProductSalesDTO>();

            var response = await _orderDetail.GetAsync<APIResponse>(1);

            if (response != null && response.IsSuccess)
            {
                listOrders = JsonConvert.DeserializeObject<List<ProductSalesDTO>>(Convert.ToString(response.Result));
            }

            return View(listOrders);

        }
    }
}
