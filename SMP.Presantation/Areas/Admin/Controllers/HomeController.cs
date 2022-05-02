using Microsoft.AspNetCore.Mvc;

namespace SMP.Presantation.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
