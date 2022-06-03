using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using SMP.Application.Extensions;
using SMP.Application.Models.DTOs;
using SMP.Application.Services.FollowService;

namespace Smp.Presantation1.Controllers
{
    public class FollowController : Controller
    {
        private readonly IFollowService _followService;

        public FollowController(IFollowService followService)
        {
            _followService = followService;
        }

        [HttpPost]

        public async Task<IActionResult> Create(CreateFollowerDTO model, string id)
        {
           
           
            model.FollowingId = id;
            model.FollowerId = User.GetUserId();
            var follow = await _followService.IsFollowExsist(model);

            if (follow != false)
            {
                TempData["Warning"] = $"The {model.FollowingId} category already exist..!";
                return RedirectToAction("User","Details");
            }
            else
            {
                model.FollowingId = id;
                model.FollowerId = User.GetUserId();
                await _followService.Create(model);
                TempData["Success"] = $"The {model.FollowingId} has been added..!";
                return RedirectToAction("User", "Details");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _followService.Delete(id,User.GetUserId());
            return RedirectToAction("User", "Details");
        }

        [HttpGet]
        public async Task<IActionResult> Followings(string id)
        {
   
            return View(await _followService.GetFollowings(id));
        }

        [HttpGet]
        public async Task<IActionResult> Followers(string id)
        {
            return View(await _followService.GetFollowers(id));
        }
    }
}
