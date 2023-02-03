using BusinessObject;
using DataAccess.Dto;
using DataAccess.Repositories;
using eStoreAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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


        //GET: apii/Product
        [HttpGet]
        public ActionResult<APIResponse> GetProducts()
        {
            try
            {
                IEnumerable<ProductDTO> productList = _repository.GetProducts();

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

        [HttpGet("{id}")]
        public ActionResult<Product> FindById([FromRoute] int id) => _repository.GetProductById(id);

        //POST: api/Product
        [HttpPost]
        public IActionResult PostProduct(Product product)
        {
            _repository.SaveProduct(product);
            return NoContent();
        }

        [HttpGet("Categories")]
        public ActionResult<IEnumerable<Category>> GetCategories() => _repository.GetCategories();

        //GET: api/Product/5
        [HttpDelete("id")]
        public IActionResult DeleteProduct(int id)
        {
            var prodFromDb = _repository.GetProductById(id);

            if (prodFromDb == null)
            {
                return NotFound();
            }

            _repository.DeleteProduct(prodFromDb);
            return NoContent();
        }

        [HttpPut("id")]
        public IActionResult UpdateProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            var prodFromDb = _repository.GetProductById(id);

            if (prodFromDb == null)
            {
                return NotFound();
            }

            _repository.UpdateProduct(product);
            return NoContent();
        }
    }
}
