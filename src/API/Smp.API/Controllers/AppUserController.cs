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
        public async Task<IActionResult> Details(string userId)
        {

            var users = await _appUserService.UserDetails(userId,User.GetUserId());
            return Ok(users);
        }

        
        [HttpGet("userIdprofile")]
        public async Task<IActionResult> UserProfile(string userIdprofile)
        {

            var user = await _appUserService.UserProfile(userIdprofile);
            return Ok(user);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO register)
        {
            if (ModelState.IsValid == true)
            {
                var result = await _appUserService.Register(register);
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
        public async  Task<IActionResult> Users()
        {



            return Ok(await _appUserService.GetUsers());





        }

  
    }
}
