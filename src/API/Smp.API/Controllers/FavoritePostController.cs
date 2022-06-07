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

        public async Task<IActionResult> Create(CreateFavoritePost model)
        {


 
            var favoritePost = await _favoritePostService.IsFavoriteExsist(model.PostId, model.UserId);

            if (favoritePost != false)
            {

                ModelState.AddModelError(String.Empty, "This post is already in your favorites..!");
                return BadRequest(ModelState);
            }
            else
            {


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


        [HttpGet("{id:string}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _favoritePostService.Delete(int.Parse(id));
            return Ok();
        }

    }
}
