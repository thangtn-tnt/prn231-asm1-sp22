using Microsoft.AspNetCore.Mvc;

namespace eStore.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
