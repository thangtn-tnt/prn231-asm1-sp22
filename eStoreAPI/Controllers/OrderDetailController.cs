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
    public class OrderDetailController : ControllerBase
    {
        private readonly APIResponse _response;
        private readonly IOrderDetailRepository _repository = new OrderDetailRepository();
        public OrderDetailController()
        {
            _response = new();
        }

        [HttpGet("{id}")]
        public ActionResult<APIResponse> GetProductSalesByMemeber(int id)
        {
            try
            {
                IEnumerable<ProductSalesDTO> productList;

                productList = _repository.GetProductSalesByMemeber(id);

                _response.Result = productList;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet]
        public ActionResult<APIResponse> GetProductSales(string startDate, string endDate)
        {

            try
            {
                if (DateTime.Parse(startDate) > DateTime.Parse(endDate))
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Start date must be less than end date");
                    return BadRequest(_response);
                }
                IEnumerable<ProductSalesDTO> productList;

                productList = _repository.GetProductSales(startDate, endDate);

                _response.Result = productList;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
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
