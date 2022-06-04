using Microsoft.AspNetCore.Mvc;
using SMP.Application.Extensions;
using SMP.Application.Models.DTOs;
using SMP.Application.Services.PostSharingService;

namespace Smp.Presantation1.Controllers
{
    public class PostSharingController : Controller
    {
        private readonly IPostSharingService _postSharingService;
        public PostSharingController(IPostSharingService postSharingService)
        {
            _postSharingService = postSharingService;

        }

        [HttpPost]

        public async Task<IActionResult> Create(PostSharingDTO model, int id)
        {


            model.PostId = id;
            model.UserId = User.GetUserId();
            var sharePost = await _postSharingService.IsRegisteredPostExsist(id);

            if (sharePost != false)
            {
                TempData["Warning"] = $"The {model.PostId} category already exist..!";
                return RedirectToAction("User", "Details");
            }
            else
            {
                model.PostId = id;
                model.UserId = User.GetUserId();
                await _postSharingService.Create(model);
                TempData["Success"] = $"The {model.PostId} has been added..!";
                return RedirectToAction("User", "Details");
            }
            
            
        


        }
        
        [HttpPost]

        public async Task<IActionResult> Delete(string id)
        {
            await _postSharingService.Delete(int.Parse(id));
            return RedirectToAction("User", "UserProfile");
        }
    }
}
