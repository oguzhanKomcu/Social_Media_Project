using Microsoft.AspNetCore.Mvc;
using SMP.Application.Extensions;
using SMP.Application.Models.DTOs;
using SMP.Application.Services.FavoritePostService;

namespace Smp.API.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritePostController : ControllerBase
    {
        private readonly IFavoritePostService _favoritePostService;


        public FavoritePostController(IFavoritePostService favoritePostService)
        {
            _favoritePostService = favoritePostService;

        }

        [HttpPost]

        public async Task<IActionResult> Create(CreateFavoritePost model, int id ,string userId)
        {


            model.PostId = id;
            model.UserId = User.GetUserId();
            var favoritePost = await _favoritePostService.IsFavoriteExsist(id, userId);

            if (favoritePost != false)
            {

                ModelState.AddModelError(String.Empty, "This post is already in your favorites..!");
                return BadRequest(ModelState);
            }
            else
            {
                model.PostId = id;
                model.UserId = User.GetUserId();
                await _favoritePostService.Create(model);

                return Ok();
            }

        }

        [HttpGet("{userId:string}")]
        public async Task<IActionResult> FavoritePosts(string userId)
        {
            var model = await _favoritePostService.GetFavoritePosts(userId);
            return Ok(model);



        }

        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _favoritePostService.Delete(id);
            return Ok();
        }

    }
}
