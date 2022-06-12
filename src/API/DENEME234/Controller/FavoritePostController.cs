using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMP.Application.Models.DTOs;
using SMP.Application.Services.FavoritePostService;

namespace SmProject.API.Controllers
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

        /// <summary>
        /// With this function, the user's profile page is returned..
        /// </summary>
        /// <param name="model">It is a required area and so type is string</param>
        /// <returns>If function is succeded will be return Ok, than will be return NotFound</returns>        
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
        /// <summary>
        /// With this function, the user's profile page is returned..
        /// </summary>
        /// <param name="userId">It is a required area and so type is string</param>
        /// <returns>If function is succeded will be return Ok, than will be return NotFound</returns>
        [HttpGet]
        public async Task<IActionResult> FavoritePosts(string userId)
        {
            var model = await _favoritePostService.GetFavoritePosts(userId);
            return Ok(model);



        }

        /// <summary>
        /// With this function, the user's profile page is returned..
        /// </summary>
        /// <param name="id">It is a required area and so type is string</param>
        /// <returns>If function is succeded will be return Ok, than will be return NotFound</returns>

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _favoritePostService.Delete(int.Parse(id));
            return Ok();
        }

    }
}
