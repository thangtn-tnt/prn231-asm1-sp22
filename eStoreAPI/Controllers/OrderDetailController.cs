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

        //[HttpGet("Categories")]
        //public ActionResult<APIResponse> GetCategories()
        //{
        //    try
        //    {
        //        IEnumerable<CategoryDTO> categories;

        //        categories = _repository.GetCategories();

        //        _response.Result = categories;
        //        _response.StatusCode = HttpStatusCode.OK;
        //        return Ok(_response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages
        //             = new List<string>() { ex.ToString() };
        //    }
        //    return _response;
        //}

        //[HttpGet("{id:int}")]
        //public ActionResult<APIResponse> GetProduct([FromRoute] int id)
        //{
        //    try
        //    {
        //        if (id == 0)
        //        {
        //            _response.StatusCode = HttpStatusCode.BadRequest;
        //            return BadRequest(_response);
        //        }

        //        var product = _repository.GetProductById(id);
        //        if (product == null)
        //        {
        //            _response.StatusCode = HttpStatusCode.NotFound;
        //            return NotFound(_response);
        //        }
        //        _response.Result = JsonConvert.SerializeObject(product);
        //        _response.StatusCode = HttpStatusCode.OK;                
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages
        //             = new List<string>() { ex.ToString() };
        //    }
        //    return _response;
        //}
        //[HttpPost]
        //public ActionResult<APIResponse> Create([FromBody] ProductCreateDTO createDTO)
        //{
        //    try
        //    {
        //        if (createDTO == null)
        //        {
        //            return BadRequest(createDTO);
        //        }

        //        _repository.SaveProduct(createDTO);
        //        _response.Result = createDTO;
        //        _response.StatusCode = HttpStatusCode.Created;                
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages
        //             = new List<string>() { ex.ToString() };
        //    }
        //    return _response;
        //}

        //[HttpDelete("{id:int}")]
        //public ActionResult<APIResponse> Delete(int id)
        //{
        //    try
        //    {
        //        if (id == 0)
        //        {
        //            return BadRequest();
        //        }
        //        var product = _repository.GetProductById(id);
        //        if (product == null)
        //        {
        //            return NotFound();
        //        }
        //        _repository.DeleteProduct(product);
        //        _response.StatusCode = HttpStatusCode.NoContent;
        //        _response.IsSuccess = true;
        //        return Ok(_response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages
        //             = new List<string>() { ex.ToString() };
        //    }
        //    return _response;
        //}

        //[HttpPut("{id:int}")]
        //public ActionResult<APIResponse> Update(int id, [FromBody] ProductUpdateDTO updateDTO)
        //{
        //    try
        //    {
        //        if (updateDTO == null || id != updateDTO.ProductId)
        //        {
        //            return BadRequest();
        //        }

        //        _repository.UpdateProduct(updateDTO);
        //        _response.StatusCode = HttpStatusCode.NoContent;
        //        _response.IsSuccess = true;                
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages
        //             = new List<string>() { ex.ToString() };
        //    }
        //    return _response;
        //}
    }
}
