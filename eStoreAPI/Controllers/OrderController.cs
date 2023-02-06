using AutoMapper;
using BusinessObject;
using DataAccess.Dto;
using DataAccess.Repositories;
using eStoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly APIResponse _response;
        private readonly IOrderRepository _order = new OrderRepository();
        private readonly IOrderDetailRepository _orderDetail = new OrderDetailRepository();
        private readonly IProductRepository _product = new ProductRepository();
        public OrderController()
        {
            _response = new();
        }


        [HttpPost]
        public ActionResult<APIResponse> Create([FromBody] OrderCreateDTO createDTO)
        {
            try
            {
                if (createDTO == null || !_product.CheckProductAvailable(createDTO) || createDTO.Discount > 100 || createDTO.Discount < 0)
                {
                    return BadRequest(createDTO);
                }

                int orderId = _order.SaveOrder(createDTO);                

                _orderDetail.SaveOrderDetail(createDTO, orderId);

                var response = _orderDetail.GetOrderDetailById(orderId);

                _response.Result = response;
                _response.StatusCode = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
