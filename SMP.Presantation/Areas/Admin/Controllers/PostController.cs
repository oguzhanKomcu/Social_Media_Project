using Microsoft.AspNetCore.Mvc;
using SMP.Application.Extensions;
using SMP.Application.Models.DTOs;
using SMP.Application.Services.PostService;

namespace SMP.Presantation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {

        private readonly IPostService _postService;
        public IActionResult Index()
        {
            return View();
        }

        public PostController(IPostService postService)
        {
            _postService = postService;
        }
        
        
        [HttpPost]
        public async Task<IActionResult> CreatePost(PostDTO model)
        {
            if (ModelState.IsValid)
            {
                if (model.User_Id == User.GetUserId())
                {
                    await _postService.Create(model);
                    return Json("Success");
                }
                else
                {
                    return Json("Failed");

                }
            }
            else
            {
                return BadRequest(String.Join(Environment.NewLine, ModelState.Values.SelectMany(h => h.Errors).Select(h => h.ErrorMessage + "" + h.Exception)));
            }
        }
    }
}
