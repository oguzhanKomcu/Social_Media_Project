using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMP.Application.Services.AppUserService;
using SMP.Application.Services.FollowService;

namespace SMProject.API.Controllers
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

        [HttpGet]
        public async Task<IActionResult> Users()
        {



            return Ok(await _appUserService.GetUsers());





        }
    }
}
