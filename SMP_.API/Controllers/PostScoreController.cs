using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMP.Application.Models.DTOs;
using SMP.Application.Services.PostScoreService;

namespace SMP_.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostScoreController : ControllerBase
    {

        private readonly IPostScoreService _postScoreService;
        public PostScoreController(IPostScoreService postScoreService)
        {
            _postScoreService = postScoreService;
        }


        [HttpPost]
        public async Task<IActionResult> PostScore([FromBody] PostScoreDTO score)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _postScoreService.Create(score);

            return Ok();
            
        }



    }
}
