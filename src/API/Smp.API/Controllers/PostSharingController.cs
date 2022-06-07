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

        [HttpDelete]

        public async Task<IActionResult> Delete(string id)
        {
            await _postSharingService.Delete(int.Parse(id));
            return Ok();
        }

    }
}
