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

        public async Task<IActionResult> Create(string comment, int postId, string userId)
        {



            if (comment == null || postId == null || userId == null)
            {

                ModelState.AddModelError(String.Empty, "Some of the required fields are 'null'. Please try again after filling in the required fields!!!");
                return BadRequest(ModelState);
            }

            else
            {
                CreatePostCommentDTO post_comment = new CreatePostCommentDTO();
                post_comment.PostId = postId;
                post_comment.UserId = userId;
                post_comment.Text = comment;


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
