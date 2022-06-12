using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMP.Application.Models.DTOs;
using SMP.Application.Services.FollowService;

namespace SmProject.API.Controllers
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
        /// <summary>
        ///In this service, the user can follow another user.
        /// </summary>
        /// <param name="model">The foreign key representing the system login user is "FollowerId". The foreign key of the user that this user wants to follow is "FollowingId".</param>
        /// <returns>If function is succeded will be return Ok, than will be return NotFound</returns>
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

        /// <summary>
        /// With this function, the user's profile page is returned..
        /// </summary>
        /// <param name="id">It is a required area and so type is string</param>
        /// <param name="userId">It is a required area and so type is string</param>
        /// <returns>If function is succeded will be return Ok, than will be return NotFound</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id, string userId)
        {
            await _followService.Delete(id, userId);
            return Ok();
        }

        /// <summary>
        /// With this function, the user's profile page is returned..
        /// </summary>
        /// <param name="followingUserId">It is a required area and so type is string</param>
        /// <returns>If function is succeded will be return Ok, than will be return NotFound</returns>
        [HttpGet("Followings")]
        public async Task<IActionResult> Followings(string followingUserId)
        {

            return Ok(await _followService.GetFollowings(followingUserId));
        }


        /// <summary>
        /// With this function, the user's profile page is returned..
        /// </summary>
        /// <param name="followerUserId">It is a required area and so type is string</param>
        /// <returns>If function is succeded will be return Ok, than will be return NotFound</returns>
        [HttpGet("Followers")]
        public async Task<IActionResult> Followers(string followerUserId)
        {
            return Ok(await _followService.GetFollowers(followerUserId));
        }
    }
}
