using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMP.Application.Models.DTOs;
using SMP.Application.Services.PostService;

namespace Smp.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;


        public PostController(IPostService postService)
        {
            _postService = postService;

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]  PostDTO model)
        {
            if (ModelState.IsValid)
            {
   
                if (model.User_Id != null)
                {
                    await _postService.Create(model);
                    return Ok();
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "A 'User Id' is required for the post. The value you enter is a 'null' value..!");
                    return BadRequest(ModelState);

                }
            }
            else
            {
                return BadRequest(String.Join(Environment.NewLine, ModelState.Values.SelectMany(h => h.Errors).Select(h => h.ErrorMessage + "" + h.Exception)));
            }
        }

        
        [HttpGet("{postId:int}")]
        public async Task<IActionResult> Details(int postId)
        {
            var model = await _postService.GetPostDetails(postId);

            return Ok(model);
        }

        [HttpGet("{userId:string}")]
        public async Task<IActionResult> UserPosts(string userId)
        {



            if (userId != null)
            {
                var model = await _postService.UserGetPosts(userId);
                return Ok(model);
            }
            else
            {

                ModelState.AddModelError(String.Empty, "This method fetches the posts according to the 'user id'. You have not entered any 'User Id' data..!");
                return BadRequest(ModelState);
            }




        }

        [HttpGet("{userId2:string}")]
        public async Task<IActionResult> AllPosts(string userId2)
        {

            if (userId2 != null)
            {
                var model = await _postService.GetPostsForMembers(userId2);
                return Ok(model);
            }
            else
            {

                ModelState.AddModelError(String.Empty, "This method fetches the posts according to the 'user id'. You have not entered any 'User Id' data..!");
                return BadRequest(ModelState);
            }




        }


        
        [HttpPut]
        public async Task<IActionResult> Update(PostDTO model)
        {
            if (ModelState.IsValid)
            {
                await _postService.Update(model);

                return Ok();


            }
            else
            {
;
                ModelState.AddModelError(String.Empty, "Post hasn't been added..!!");
                return BadRequest(ModelState);                
            }

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _postService.Delete(int.Parse(id));
            return Ok();
        }



    }
}
