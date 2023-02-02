using Microsoft.AspNetCore.Mvc;

namespace eStore.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
