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

        public AppUserController(IAppUserService appUserService)
        {


            _appUserService = appUserService;
        }

        /// <summary>
        /// This function returns the user whose "id" is given.
        /// </summary>
        /// <param name="userId">It is a required area and so type is string</param>
        /// <returns>If function is succeded will be return Ok, than will be return NotFound</returns>
        [HttpGet("{userId:string}")]
        public async Task<IActionResult> Details(string userId)
        {

            var user = await _appUserService.UserDetails(userId,User.GetUserId());
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        /// <summary>
        /// With this function, the user's profile page is returned..
        /// </summary>
        /// <param name="userIdprofile">It is a required area and so type is string</param>
        /// <returns>If function is succeded will be return Ok, than will be return NotFound</returns>

        [HttpGet("{userIdprofile:string}")]
        public async Task<IActionResult> UserProfile(string userIdprofile)
        {

            var user = await _appUserService.UserProfile(userIdprofile);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
 
        }
        /// <summary>
        /// You can add a new user using this method.
        /// </summary>
        /// <returns>If function is succeded will be return CreatedAtAction, than will be return Bad Request</returns>    
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

        /// <summary>
        /// You can get tokens by entering your username and password.
        /// </summary>
        /// <returns></returns>
        [HttpPost("user")]

        public IActionResult Login(/*[FromRoute]*/ LoginDTO user)
        {
            var token = _appUserService.Authentication(user.UserName, user.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(new { token, user });

        }



        [HttpPost]
        public async Task<IActionResult> logOut()
        {
            await _appUserService.LogOut();
            return Ok();

        }



        /// <summary>
        /// You can update the user by entering the desired fields.
        /// </summary>
        /// <returns></returns>

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


        /// <summary>
        /// With this function you can fetch all user.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async  Task<IActionResult> Users()
        {



            return Ok(await _appUserService.GetUsers());





        }

  
    }
}
