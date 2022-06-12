using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMP.Application.Models.DTOs;
using SMP.Application.Services.PostService;

namespace Smp.API.Controllers
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

        
        /// <summary>
        /// With this function, the user's profile page is returned..
        /// </summary>
        /// <param name="model">It is a required area and so type is string</param>
        /// <returns>If function is succeded will be return Ok, than will be return NotFound</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PostDTO model)
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

        /// <summary>
        /// With this function, the user's profile page is returned..
        /// </summary>
        /// <param name="postId">It is a required area and so type is string</param>
        /// <returns>If function is succeded will be return Ok, than will be return NotFound</returns>
        [HttpGet("Details")]
        public async Task<IActionResult> Details(int postId)
        {
            var model = await _postService.GetPostDetails(postId);

            return Ok(model);
        }

        /// <summary>
        /// With this function, the user's profile page is returned..
        /// </summary>
        /// <param name="userId">It is a required area and so type is string</param>
        /// <returns>If function is succeded will be return Ok, than will be return NotFound</returns>
        [HttpGet("UserPosts")]
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
        /// <summary>
        /// With this function, the user's profile page is returned..
        /// </summary>
        /// <param name="userIdAllPosts">It is a required area and so type is string</param>
        /// <returns>If function is succeded will be return Ok, than will be return NotFound</returns>
        [HttpGet("AllPosts")]
        public async Task<IActionResult> AllPosts(string userIdAllPosts)
        {

            if (userIdAllPosts != null)
            {
                var model = await _postService.GetPostsForMembers(userIdAllPosts);
                return Ok(model);
            }
            else
            {

                ModelState.AddModelError(String.Empty, "This method fetches the posts according to the 'user id'. You have not entered any 'User Id' data..!");
                return BadRequest(ModelState);
            }




        }


        /// <summary>
        /// With this function, the user's profile page is returned..
        /// </summary>
        /// <param name="model">It is a required area and so type is string</param>
        /// <returns>If function is succeded will be return Ok, than will be return NotFound</returns>
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
        /// <summary>
        /// With this function, the user's profile page is returned..
        /// </summary>
        /// <param name="id">It is a required area and so type is string</param>
        /// <returns>If function is succeded will be return Ok, than will be return NotFound</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _postService.Delete(int.Parse(id));
            return Ok();
        }



    }
}