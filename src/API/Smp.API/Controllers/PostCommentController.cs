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


        [HttpGet("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _postCommentService.Delete(id);
            return Ok();

        }
    }
}
