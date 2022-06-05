using Microsoft.AspNetCore.Mvc;
using SMP.Application.Extensions;
using SMP.Application.Models.DTOs;
using SMP.Application.Services.PostService;

namespace Smp.Presantation1.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;


        public PostController(IPostService postService)
        {
            _postService = postService;

        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PostDTO model)
        {
            if (ModelState.IsValid)
            {
                model.User_Id = User.GetUserId();
                if (model.User_Id == User.GetUserId())
                {
                    await _postService.Create(model);
                    return RedirectToAction("UserPosts");
                }
                else
                {
                    return RedirectToAction("Create");

                }
            }
            else
            {
                return BadRequest(String.Join(Environment.NewLine, ModelState.Values.SelectMany(h => h.Errors).Select(h => h.ErrorMessage + "" + h.Exception)));
            }
        }

        public async Task<IActionResult> Details(int id)
        {

           return  View(await _postService.GetPostDetails(id));
        }
        public async Task<IActionResult> UserPosts()
        {

           
            var model = await _postService.UserGetPosts(User.GetUserId());
            return View(model);



        }

        [HttpGet]
        public async Task<IActionResult> AllPosts()
        {
            var model = await _postService.GetPostsForMembers(User.GetUserId());


            return View(model);



        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _postService.GetById(id);
            ViewData["uploadPath"] = model.ImagePath;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(PostDTO model)
        {
            if (ModelState.IsValid)
            {
                await _postService.Update(model);
                TempData["Success"] = "Post has been added..!!";
                return RedirectToAction("UserProfile","User");


            }
            else
            {
                TempData["Error"] = "Post hasn't been added..!!";
                return View(model);
            }

        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _postService.Delete(id);
            return RedirectToAction("User", "UserProfile");
        }

       



    }



}
