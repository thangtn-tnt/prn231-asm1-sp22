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
    public class ProductController : ControllerBase
    {
        private readonly APIResponse _response;
        private readonly IProductRepository _repository = new ProductRepository();
        public ProductController()
        {
            _response = new();
        }

        [HttpGet]
        public ActionResult<APIResponse> GetProducts([FromQuery] string? search)
        {
            try
            {
                IEnumerable<ProductDTO> productList;

                productList = _repository.GetProducts();

                if (!string.IsNullOrEmpty(search))
                {
                    productList = productList.Where(u => u.ProductName.ToLower().Contains(search));
                }

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

        [HttpGet("Categories")]
        public ActionResult<APIResponse> GetCategories()
        {
            try
            {
                IEnumerable<CategoryDTO> categories;

                categories = _repository.GetCategories();

                _response.Result = categories;
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

        [HttpGet("{id:int}")]
        public ActionResult<APIResponse> GetProduct([FromRoute] int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var product = _repository.GetProductById(id);
                if (product == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = product;
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
        [HttpPost]
        public ActionResult<APIResponse> Create([FromBody] ProductCreateDTO createDTO)
        {
            try
            {
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                ProductDTO model = new()
                {
                    ProductName = createDTO.ProductName,                    
                };

                _repository.SaveProduct(createDTO);
                _response.Result = createDTO;
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
