using Microsoft.AspNetCore.Mvc;
using SMP.Application.Services.AppUserService;

namespace SMP.Presantation.Areas.User.Controllers
{

    [Area("Admin")]    
    public class ProfileController : Controller
    {

        private readonly IAppUserService _appuserService;
        public ProfileController(IAppUserService appUserService)
        {
            _appuserService = appUserService;
        }


        public async Task<IActionResult> Detail(string id)
        {
            return View();
        }
    }
}
