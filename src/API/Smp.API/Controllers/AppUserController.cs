using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMP.Application.Extensions;
using SMP.Application.Models.DTOs;
using SMP.Application.Services.AppUserService;
using SMP.Application.Services.FollowService;

namespace Smp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserService _appUserService;
        private readonly IFollowService _followService;
        public AppUserController(IAppUserService appUserService, IFollowService followService)
        {
            _followService = followService;

            _appUserService = appUserService;
        }

        [HttpGet("userId")]
        public async Task<IActionResult> Details(string userId )
        {

            var users = await _appUserService.UserDetails(userId);
            return Ok(users);
        }

        
        [HttpGet("userId1")]
        public async Task<IActionResult> UserProfile(string userId1)
        {

            var user = await _appUserService.UserDetails(userId1);
            return Ok(user);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO register)
        {
            if (ModelState.IsValid == true)
            {
                var result = await _appUserService.Register(register);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(String.Empty, item.Description);
                    return BadRequest(ModelState);
                }
            }

            return Ok(register);
        }




        [HttpPost("Login")]
        public async Task<IActionResult> LogIn(LoginDTO Login)
        {

            if (ModelState.IsValid)
            {
                var result = await _appUserService.Login(Login);
                return Ok();
            }


            ModelState.AddModelError(String.Empty, "You have not entered the correct username and password . Please try again!!");
            return BadRequest(ModelState);
        }



        [HttpPost]
        public async Task<IActionResult> logOut()
        {
            await _appUserService.LogOut();
            return Ok();

        }

        //[HttpGet("EditUserId")]
        //public async Task<IActionResult> Edit(string EditUserId)
        //{
        //    if (EditUserId == User.GetUserId())
        //    {
        //        var user = await _appUserService.GetById(EditUserId);

        //        if (user == null)
        //        {
        //            ModelState.AddModelError(String.Empty, "You have not entered the correct username and password . Please try again!!");
        //            return BadRequest(ModelState);
        //        }
        //        else
        //        {


        //            return Ok(user);
        //        }

        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        //}


        [HttpPut]
        public async Task<IActionResult> Edit(UpdateProfilDTO model)
        {
            if (ModelState.IsValid)
            {
                await _appUserService.UpdateUser(model);

                return Ok();
            }
            else
            {


                ModelState.AddModelError(String.Empty, "You have not entered a correct model !!!");
                return BadRequest(ModelState);
            }


        }



        [HttpGet]
        public async Task<IActionResult> Users()
        {



            return Ok(await _appUserService.GetUsers());





        }

        //[HttpPost]
        //public async Task<IActionResult> Users(string userName)
        //{
        //    if (!string.IsNullOrEmpty(userName))
        //    {
        //        return View(await _appUserService.GetUserName(userName));
        //    }
        //    else
        //    {
        //        return View(await _appUserService.GetUsers());
        //    }





        //}
    }
}
