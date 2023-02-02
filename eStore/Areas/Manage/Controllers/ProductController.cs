using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessObject;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eStore.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ProductController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = string.Empty;
        private readonly JsonSerializerOptions options;

        public ProductController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:44318/api/Product";

            options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        [ValidateAntiForgeryToken]
        public async Task<Product> GetProductById(int id)
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl + "/" + id);

            string strData = await response.Content.ReadAsStringAsync();

            Product product = !string.IsNullOrEmpty(strData) ? JsonSerializer.Deserialize<Product>(strData, options) : null;

            return product;
        }

        [ValidateAntiForgeryToken]
        public async Task<List<Category>> GetCategories()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl + "/Categories");

            string strData = await response.Content.ReadAsStringAsync();

            List<Category> cate = !string.IsNullOrEmpty(strData) ? JsonSerializer.Deserialize<List<Category>>(strData, options) : null;

            return cate;
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);

            string strData = await response.Content.ReadAsStringAsync();

            List<Product> listProducts = !string.IsNullOrEmpty(strData) ? JsonSerializer.Deserialize<List<Product>>(strData, options) : null;

            return View(listProducts);
        }

        //GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            return View();
        }

        //GET: ProductController/Update/5
        public async Task<IActionResult> Update(int id)
        {
            ViewData["CategoryId"] = new SelectList(await GetCategories(), "CategoryId", "CategoryName");
            return View(await GetProductById(id));
        }

        //POST: ProductController/Update/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(int id, Product collection)
        {
            ViewData["CategoryId"] = new SelectList(await GetCategories(), "CategoryId", "CategoryName");

            return View();
        }

        //GET: ProductController/Delete/5
        public async Task<ActionResult> Delete(int id) => View(await GetProductById(id));

        //POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            return View();

        }

    }
}

