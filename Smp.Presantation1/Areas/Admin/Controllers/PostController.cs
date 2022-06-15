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

        [HttpGet]
        public async Task<IActionResult> AllPostsUser(string userName)
        {
            var model = await _postService.GetAllPostsUserName(userName);


            return View(model);



        }
    }
}
