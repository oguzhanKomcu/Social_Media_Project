using Microsoft.AspNetCore.Mvc;
using SMP.Application.Extensions;
using SMP.Application.Models.DTOs;
using SMP.Application.Services.PostService;
using SMP.Application.Services.PostSharingService;
using SMP.Domain.Models.Entities;

namespace SMP.Presantation.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IPostSharingService _postSharingService;

        public PostController(IPostService postService, IPostSharingService postSharingService)
        {
            _postService = postService;
            _postSharingService = postSharingService;
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
                    return RedirectToAction("List");
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
        public async Task<IActionResult> UserPosts()
        {
            var model = await _postService.UserGetPosts(User.GetUserId());
            return View(model);



        }

        [HttpGet]
        public async Task<IActionResult> AllPosts()
        {
           var model = await _postService.GetPostsForMembers();
           return View(model);
         


        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            return View(await _postService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(PostDTO model)
        {
            if (ModelState.IsValid)
            {
                await _postService.Update(model);
                TempData["Success"] = "Post has been added..!!";
                return RedirectToAction("List");


            }
            else
            {
                TempData["Error"] = "Post hasn't been added..!!";
                return View(model);
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            await _postService.Delete(id);
            return RedirectToAction("List");
        }



    }



}
