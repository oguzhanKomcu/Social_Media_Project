using Microsoft.AspNetCore.Mvc;
using SMP.Application.Models.DTOs;
using SMP.Application.Services.AppUserService;

namespace Smp.Presantation1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {

        private readonly IAppUserService _appUserService;


        public UserController(IAppUserService appUserService)
        {
   

            _appUserService = appUserService;

        }



        [HttpGet]
        public async Task<IActionResult> Users(string userName)
        {
            return View(await _appUserService.GetUserName(userName));




        }

        
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _appUserService.Delete(id);
            return RedirectToAction("User", "Users");
        }

        //[AllowAnonymous]
        public IActionResult LogIn(string returnUrl = "/")
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();

        }


        [HttpPost]
        public async Task<IActionResult> LogIn(LoginDTO model)
        {

            if (ModelState.IsValid)
            {
                var result = await _appUserService.Login(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("AllPostsUser", "Post");
                }

                TempData["Warning"] = "Invalid log in credantial..!!";
            }



            return RedirectToAction("Index", "Home");
        }




        private IActionResult RedirectToLocal(string returnUrl)
        {



            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        public async Task<IActionResult> logOut()
        {
            await _appUserService.LogOut();
            //await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }        
    }
}
