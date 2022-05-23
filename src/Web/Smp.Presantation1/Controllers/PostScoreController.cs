using Microsoft.AspNetCore.Mvc;
using SMP.Application.Extensions;
using SMP.Application.Models.DTOs;
using SMP.Application.Services.PostScoreService;
using SMP.Domain.Models.Entities;

namespace Smp.Presantation1.Controllers
{
    public class PostScoreController : Controller
    {
        private readonly IPostScoreService _postScore;
        public PostScoreController(IPostScoreService postScore)
        {
            _postScore = postScore;
        }



        [HttpPost]

        public async Task<IActionResult> Create(string score, int postId)
        {
            string id = User.GetUserId();
            var score1 = await _postScore.IsScoreExsist(postId,id);

            if (score1 != false)
            {
                TempData["Warning"] = $"The {id} category already exist..!";
                return RedirectToAction("User", "Details");
            }
            else
            {
                PostScoreDTO post_Score = new PostScoreDTO();
                post_Score.PostId = postId;
                post_Score.UserId = id;
                post_Score.Score = Convert.ToDecimal(score, System.Globalization.CultureInfo.InvariantCulture);


                await _postScore.Create(post_Score);
                TempData["Success"] = $"The {post_Score.PostId} has been added..!";
                return RedirectToAction("User", "Details");
            }
        }
    }
}
