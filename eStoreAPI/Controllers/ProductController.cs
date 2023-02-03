using BusinessObject;
using DataAccess.Dto;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository = new ProductRepository();

        //GET: apii/Product
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> GetProducts() => _repository.GetProducts();

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
