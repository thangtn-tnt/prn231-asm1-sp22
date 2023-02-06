using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
