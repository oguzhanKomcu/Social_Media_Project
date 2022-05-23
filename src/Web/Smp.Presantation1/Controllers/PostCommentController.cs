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
                post_comment.Post_Id = postId;
                post_comment.User_Id = User.GetUserId();
                post_comment.Text = comment;


                await _postCommentService.Create(post_comment);
                TempData["Success"] = $"The {post_comment.Post_Id} has been added..!";
                return RedirectToAction("User", "Details");
            }
        }


    }
}
