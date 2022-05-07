using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMP.Application.Models.DTOs;
using SMP.Application.Services.PostService;

namespace SMP_API.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;

        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostDTO model)
        {
            if (ModelState.IsValid)
            {
                await _postService.Create(model);
                ModelState.AddModelError(String.Empty, "The category has been created..!");
                return Ok(ModelState);
            }
            else
            {
                ModelState.AddModelError(String.Empty, "The category hasn't been created..!");
                return BadRequest(ModelState);
            }

        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var category = await _postService.GetById(id);

            return Ok(category);
        }

        [HttpGet("{id:string}")]
        public async Task<IActionResult> GetUserPosts(string id)
        {
            var categories = await _postService.UserGetPosts(id);

            return Ok(categories);
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var categories = await _postService.GetPostsForMembers();

            return Ok(categories);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePostDTO model)
        {
            if (ModelState.IsValid)
            {
                await _postService.Update(model);
                ModelState.AddModelError(String.Empty, "The category has been created..!");
                return Ok(ModelState);
            }
            else
            {
                ModelState.AddModelError(String.Empty, "The category hasn't been created..!");
                return BadRequest(ModelState);
            }

        }

        [HttpDelete("Id")]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (id != null)
            {

                await _postService.Delete(id);
                ModelState.AddModelError(string.Empty, $"Something went wrong deleting record");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }
    }
}
