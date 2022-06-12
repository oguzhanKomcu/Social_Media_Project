using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMP.Application.Extensions;
using SMP.Application.Models.DTOs;
using SMP.Application.Services.PostScoreService;
using SMP.Domain.Models.Entities;

namespace Smp.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PostScoreController : ControllerBase
    {
        private readonly IPostScoreService _postScore;
        public PostScoreController(IPostScoreService postScore)
        {
            _postScore = postScore;
        }


        /// <summary>
        /// With this function, the user's profile page is returned..
        /// </summary>
        /// <param name="post_Score">It is a required area and so type is string</param>
        /// <returns>If function is succeded will be return Ok, than will be return NotFound</returns>
        [HttpPost]
        public async Task<IActionResult> Create(PostScoreDTO post_Score)
        {

            decimal score1 = Convert.ToDecimal(post_Score.Score, System.Globalization.CultureInfo.InvariantCulture);
            if (10 >= score1 || score1 < 0)
            {



                var score2 = await _postScore.IsScoreExsist(post_Score.PostId, post_Score.UserId);

                if (score2 != false)
                {
                    ModelState.AddModelError(String.Empty, "Give any number between 10 and 0!");
                    return BadRequest(ModelState);
                }
                else
                {

                    post_Score.Score = Convert.ToDecimal(post_Score.Score, System.Globalization.CultureInfo.InvariantCulture);

                    Post post = new Post();
                    AppUser appUser = new AppUser();
                    await _postScore.Create(post_Score, post, appUser);
                    return Ok();








                }
            }
            else
            {
                ModelState.AddModelError(String.Empty, "You have given a score in this post. You cannot give again. !");
                return BadRequest(ModelState);
            }




        }
    }
}