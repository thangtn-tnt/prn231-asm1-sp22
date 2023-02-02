using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    public class ManageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
