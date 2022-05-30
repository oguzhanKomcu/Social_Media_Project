using Microsoft.AspNetCore.Mvc;
using SMP.Application.Extensions;
using SMP.Application.Models.DTOs;
using SMP.Application.Services.PostCommentService;

namespace Smp.Presantation1.Controllers
{
    public class PostCommentController : Controller
    {
        private readonly IPostCommentService _postCommentService;
        public PostCommentController(IPostCommentService postCommentService)
        {
            _postCommentService = postCommentService;
        }

        
        [HttpPost]

        public async Task<IActionResult> Create(string comment, int postId)
        {



            if (comment == null)
            {
                TempData["Warning"] = $"The {comment} category already exist..!";
                return RedirectToAction("User", "Details");
            }
            else
            {
                CreatePostCommentDTO post_comment = new CreatePostCommentDTO();
                post_comment.PostId = postId;
                post_comment.UserId = User.GetUserId();
                post_comment.Text = comment;


                await _postCommentService.Create(post_comment);
                TempData["Success"] = $"The {post_comment.PostId} has been added..!";
                return RedirectToAction("Details", "Post", routeValues: new { id = postId });
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _postCommentService.Delete(id);
            return RedirectToAction("Details", "Post");

        }

    }
}
