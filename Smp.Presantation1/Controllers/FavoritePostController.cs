using Microsoft.AspNetCore.Mvc;
using SMP.Application.Extensions;
using SMP.Application.Models.DTOs;
using SMP.Application.Services.FavoritePostService;

namespace Smp.Presantation1.Controllers
{
    public class FavoritePostController : Controller
    {
        private readonly IFavoritePostService _favoritePostService;
        public FavoritePostController(IFavoritePostService favoritePostService)
        {
            _favoritePostService = favoritePostService;

        }

        [HttpPost]

        public async Task<IActionResult> Create(CreateFavoritePost model, int id)
        {


            model.PostId = id;
            model.UserId = User.GetUserId();
            var favoritePost = await _favoritePostService.IsFavoriteExsist(id, User.GetUserId());

            if (favoritePost != false)
            {
                TempData["Warning"] = $"The {model.PostId} category already exist..!";
                return RedirectToAction("User", "Details");
            }
            else
            {
                model.PostId = id;
                model.UserId = User.GetUserId();
                await _favoritePostService.Create(model);
                TempData["Success"] = $"The {model.PostId} has been added..!";
                return RedirectToAction("User", "Details");
            }

        }
        
        [HttpGet]
        public async Task<IActionResult> FavoritePosts()
        {
            var model = await _favoritePostService.GetFavoritePosts(User.GetUserId());
            return View(model);



        }

    }
}
