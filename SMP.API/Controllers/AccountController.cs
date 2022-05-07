using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMP.Application.Models.DTOs;
using SMP.Application.Services.AppUserService;
using SMP.Domain.Models.Entities;

namespace SMP.API.Controllers
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


        
  
//  "userName": "gamze",
//  "password": "123",
//  "confirmPassword": "123",
//  "email": "gamzw@gmail.com",
//  "status": 1
//}

    //[HttpPost("Register")]
    //public async Task<IActionResult> Register([FromBody] RegisterDTO user)
    //{
    //    if (user is null)
    //    {
    //        return BadRequest();
    //    }

    //    await _appuserService.Register(user);

    //    return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
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
        //[Route("api/[controller]")]
        //[ApiController]
        //public class PostController : ControllerBase
        //{

        //    private readonly IPostService _postService;

        //    public PostController(IPostService postService)
        //    {
        //        _postService = postService;

        //    }



        //    /// <summary>
        //    /// This function lists all made reservations.
        //    /// </summary>
        //    /// <returns></returns>
        //    [HttpGet("{id:int}")]
        //    public async Task<IActionResult> UserGetPosts(string id)
        //    {
        //        return Ok(await _postService.UserGetPosts(id).ConfigureAwait(false));
        //    }



        //    /// <summary>
        //    /// This function lists all made reservations.
        //    /// </summary>
        //    /// <returns></returns>

        //    [HttpGet("{id:length(24)}")]
        //    public async Task<IActionResult> GetById(int id)
        //    {
        //        var customer = await _postService.GetById(id);

        //        if (customer is null)
        //        {
        //            return NotFound();
        //        }


        //        return Ok(customer);


        //    }


        //    /// <summary>
        //    /// This function lists all made reservations.
        //    /// </summary>
        //    /// <returns></returns>
        //    [HttpPost]
        //    public async Task<IActionResult> Create([FromBody] CreatePostDTO model)
        //    {



        //        if (model is null)
        //        {
        //            return BadRequest();
        //        }

        //        await _postService.Create(model);

        //        return Ok(ModelState);
        //    }



        //    /// <summary>
        //    /// This function lists all made reservations.
        //    /// </summary>
        //    /// <returns></returns>

        //    [HttpPut]
        //    public async Task<IActionResult> PutPost([FromBody] UpdatePostDTO model)
        //    {



        //        if (model is null)
        //        {
        //            return BadRequest();
        //        }

        //        await _postService.Update(model);

        //        return Ok(ModelState);
        //    }



        //    /// <summary>
        //    /// This function lists all made reservations.
        //    /// </summary>
        //    /// <returns></returns>
        //    [HttpDelete]
        //    public async Task<IActionResult> DeletePost(int id)
        //    {
        //        await _postService.Delete(id);
        //        return Ok();
        //    }

        //}
    }
}
