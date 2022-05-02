using Microsoft.AspNetCore.Mvc;

namespace SMP.Presantation.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
