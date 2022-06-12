using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMP.Application.Extensions;
using SMP.Application.Models.DTOs;
using SMP.Application.Services.PostSharingService;

namespace Smp.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PostSharingController : ControllerBase
    {

        private readonly IPostSharingService _postSharingService;
        public PostSharingController(IPostSharingService postSharingService)
        {
            _postSharingService = postSharingService;

        }


        /// <summary>
        /// With this function, the user's profile page is returned..
        /// </summary>
        /// <param name="model">It is a required area and so type is string</param>
        /// <returns>If function is succeded will be return Ok, than will be return NotFound</returns>
        [HttpPost]

        public async Task<IActionResult> Create(PostSharingDTO model)
        {

            var sharePost = await _postSharingService.IsRegisteredPostExsist(model.PostId);

            if (sharePost != false)
            {
                ModelState.AddModelError(String.Empty, "You shared this post. You cannot share it on your profile again!");
                return BadRequest(ModelState);
            }
            else
            {

                await _postSharingService.Create(model);
                return Ok();
            }





        }
        /// <summary>
        /// With this function, the user's profile page is returned..
        /// </summary>
        /// <param name="id">It is a required area and so type is string</param>
        /// <returns>If function is succeded will be return Ok, than will be return NotFound</returns>
        [HttpDelete]

        public async Task<IActionResult> Delete(string id)
        {
            await _postSharingService.Delete(int.Parse(id));
            return Ok();
        }

    }
}