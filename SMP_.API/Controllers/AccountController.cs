using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMP.Application.Models.DTOs;
using SMP.Application.Services.AppUserService;
using SMP.Domain.Models.Entities;

namespace SMP_.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAppUserService _appuserService;

        public AccountController(IAppUserService appUserService)
        {
            _appuserService = appUserService;

        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO user)
        {
            if (ModelState.IsValid)
            {
              

                var result = _appuserService.Register(user);

                if (result == null)
                {
                    return BadRequest();
                }
                return Ok(result);
            }
            return BadRequest();
        }


        [HttpGet("{id:length(24)}")]
        public ActionResult<AppUser> GetUser(string id)
        {
            var user = _appuserService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _appuserService.GetUsers().ConfigureAwait(false));
        }
    }
}
