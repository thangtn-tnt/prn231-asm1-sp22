using Microsoft.AspNetCore.Mvc;

namespace eStore.Areas.Manage.Controllers
{
    public class OrderDetailController : Controller
    {
        [Area("Manage")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
