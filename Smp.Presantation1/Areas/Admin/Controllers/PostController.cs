using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMP.Application.Services.PostService;

namespace Smp.Presantation1.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;


        public PostController(IPostService postService)
        {
            _postService = postService;

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AllPostsUser(string userName)
        {
            var model = await _postService.GetAllPostsUserName(userName);


            return View(model);



        }

        public async Task<IActionResult> Details(int id)
        {

            return View(await _postService.GetPostDetails(id));
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _postService.Delete(id);
            return RedirectToAction("AllPostsUser", "Post");
        }
    }
}
