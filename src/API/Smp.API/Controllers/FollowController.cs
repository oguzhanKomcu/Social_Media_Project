using Microsoft.AspNetCore.Mvc;
using SMP.Application.Models.DTOs;
using SMP.Application.Services.FollowService;

namespace Smp.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IFollowService _followService;

        public FollowController(IFollowService followService)
        {
            _followService = followService;
        }

        [HttpPost]

        public async Task<IActionResult> Create(CreateFollowerDTO model)
        {



            var follow = await _followService.IsFollowExsist(model);

            if (follow != false)
            {
                
                ModelState.AddModelError(String.Empty, "This person is following this user.It can't be traced back.!");
                return BadRequest(ModelState);
            }
            else
            {

                await _followService.Create(model);
                return Ok();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id, string userId)
        {
            await _followService.Delete(id, userId);
            return Ok();
        }

        [HttpGet("{followingUserId:string}")]
        public async Task<IActionResult> Followings(string followingUserId)
        {

            return Ok(await _followService.GetFollowings(followingUserId));
        }


        [HttpGet("{followerUserId:string}")]
        public async Task<IActionResult> Followers(string followerUserId)
        {
            return Ok(await _followService.GetFollowers(followerUserId));
        }
    }
}
