using Microsoft.AspNetCore.Mvc;
using SMP.Application.Extensions;
using SMP.Application.Models.DTOs;
using SMP.Application.Models.VMs;
using SMP.Application.Services.AppUserService;
using SMP.Application.Services.FollowService;
using SMP.Domain.Models.Entities;

namespace Smp.Presantation1.Controllers
{
    public class UserController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IFollowService _followService;
        public UserController(IAppUserService appUserService, IFollowService followService)
        {
            _followService = followService;
            
            _appUserService = appUserService;
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id  == User.GetUserId())
            {
                return RedirectToAction("UserProfile", "User");
            }
            var users = await _appUserService.UserDetails(id,User.GetUserId());
            return View(users);
        }

        public async Task<IActionResult> UserProfile()
        {

            var user = await _appUserService.UserProfile(User.GetUserId());
            return View(user);
        }
        


        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated) 
            {
                return RedirectToAction("Index", "Home"); 


               
            }


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (ModelState.IsValid == true)
            {
                var result = await _appUserService.Register(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(String.Empty, item.Description);
                    TempData["Warning"] = $"{item.Description}";
                }
            }

            return View(model);
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
                    return RedirectToAction("AllPosts", "Post");
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
            return RedirectToAction("Index", "Home");

        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == User.GetUserId())
            {
                var user = await _appUserService.GetById(User.GetUserId()); 

                if (user == null)
                {
                    return NotFound();
                }
                else
                {


                    return View(user);
                }

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }


        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProfilDTO model)
        {
            if (ModelState.IsValid)
            {
                await _appUserService.UpdateUser(model);
                TempData["Success"] = "Your profile has been edited..!!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Error"] = "Your profile hasn't been edited..!!";
                return RedirectToAction("Edit");
            }


        }



        //[HttpGet]
        //public async Task<IActionResult> Users()
        //{



        //    return View(await _appUserService.GetUsers());





        //}

        [HttpGet]
        public async Task<IActionResult> Users(string userName)
        {
            return View(await _appUserService.GetUserName(userName));




        }
        //[HttpGet]
        //public async  ActionResult Users(string searchString)
        //{
        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        var user = await _appUserService.GetUserName(searchString);
     
        //    }
      
        //    return View(user);
        //}
    }
}
