using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMP.Application.Models.DTOs;
using SMP.Application.Services.PostService;

namespace SMP_.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;

        }


        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetById(string id)
        {
            
            var post = await _postService.GetById(int.Parse(id));

            if (post is null)
            {
                return NotFound();
            }


            return Ok(post);


        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PostDTO post)
        {
            if (post is null)
            {
                return BadRequest();
            }

            await _postService.Create(post);

            return CreatedAtAction(nameof(GetById), new { id = post.Id }, post);
        }
    }
}
