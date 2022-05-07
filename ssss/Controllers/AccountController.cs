using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMP.Application.Models.DTOs;
using SMP.Application.Services.AppUserService;
using SMP.Domain.Models.Entities;

namespace ssss.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAppUserService _appuserService;

        public AccountController(IAppUserService appUserService)
        {
            _appuserService = appUserService;

        }

        //[HttpPost("Register")]
        //public async Task<IActionResult> Register([FromBody] RegisterDTO user)
        //{
        //    if (user is null)
        //    {
        //        return BadRequest();
        //    }

        //    await _appuserService.Register(user);

        //    return CreatedAtAction(nameof(GetUser), new { user);
        //}
        
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

    }
}
