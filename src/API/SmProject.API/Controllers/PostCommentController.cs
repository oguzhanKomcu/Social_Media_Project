using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMP.Application.Models.DTOs;
using SMP.Application.Services.PostCommentService;

namespace Smp.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PostCommentController : ControllerBase
    {
        private readonly IPostCommentService _postCommentService;
        public PostCommentController(IPostCommentService postCommentService)
        {
            _postCommentService = postCommentService;
        }

        /// <summary>
        /// With this function, the user's profile page is returned..
        /// </summary>
        /// <param name="post_comment">It is a required area and so type is string</param>
        /// <returns>If function is succeded will be return Ok, than will be return NotFound</returns>
        [HttpPost]

        public async Task<IActionResult> Create(CreatePostCommentDTO post_comment)
        {



            if (post_comment.Text == null || post_comment.PostId == null || post_comment.UserId == null)
            {

                ModelState.AddModelError(String.Empty, "Some of the required fields are 'null'. Please try again after filling in the required fields!!!");
                return BadRequest(ModelState);
            }

            else
            {



                await _postCommentService.Create(post_comment);

                return Ok();
            }
        }


        /// <summary>
        /// With this function, the user's profile page is returned..
        /// </summary>
        /// <param name="id">It is a required area and so type is string</param>
        /// <returns>If function is succeded will be return Ok, than will be return NotFound</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _postCommentService.Delete(id);
            return Ok();

        }
    }
}