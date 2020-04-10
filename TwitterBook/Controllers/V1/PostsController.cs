using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwitterBook.Constants.V1;
using TwitterBook.Contracts.V1.Request;
using TwitterBook.Contracts.V1.Response;
using TwitterBook.Extensions;
using TwitterBook.Models;
using TwitterBook.Services.PostService;

namespace TwitterBook.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Produces("application/json")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService postService;

        public PostsController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpGet(APIRoute.Post.GetAll)]
        public async Task<IActionResult> GetAll()
        {

            return Ok(await postService.GetPosts());
        }
        /// <summary>
        /// Return all Posts
        /// </summary>
        /// <param name="postId"></param>
        [HttpGet(APIRoute.Post.Get)]
        [Authorize(Policy = "WorkForCompanyRequirement")]
        public async Task<IActionResult> GetAll([FromRoute] Guid postId)
        {

            var ownsPost = await postService.CheckUserOwnPost(postId, HttpContext.GetUserId());

            if (!ownsPost)
                return BadRequest(new { Error = "Your not owns this post" });

            var post = await postService.GetPostById(postId);

            if (post == null)
                return NotFound();

            return Ok(post);
        }
        /// <summary>
        /// Create the post item
        /// </summary>
        /// <response code="201">Created</response>
        /// <response code="401">Unathorized</response>
        /// <response code="403">Forbidden</response>
        [HttpPost(APIRoute.Post.Create)]
        [ProducesResponseType(typeof(PostResponse), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 403)]

        public async Task<IActionResult> Create([FromBody] PostRequest postRequest)
        {



            var post = new Post() { Id = Guid.NewGuid(), Name = postRequest.Name, UserId = HttpContext.GetUserId() };


            await postService.CreatePost(post);

            var baseURL = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var location = $"{baseURL}/{APIRoute.Post.Get.Replace("{postId}", post.Id.ToString())}";

            var postResponse = new PostResponse() { Id = post.Id };

            return Created(location, postResponse);
        }

        [Authorize(policy: "UpdatePost")]
        [HttpPut(APIRoute.Post.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid postId, [FromBody]  PostRequest postRequest)
        {
            var ownsPost = await postService.CheckUserOwnPost(postId, HttpContext.GetUserId());

            if (!ownsPost)
                return BadRequest(new { Error = "Your not owns this post" });

            var post = new Post()
            {
                Id = postId,
                Name = postRequest.Name
            };

            var updated = await postService.UpdatePost(post);


            if (!updated)
                return NotFound();

            return Ok(post);
        }


        [HttpDelete(APIRoute.Post.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid postId)
        {
            var ownsPost = await postService.CheckUserOwnPost(postId, HttpContext.GetUserId());

            if (!ownsPost)
                return BadRequest(new { Error = "Your not owns this post" });

            var deleted = await postService.DeletePost(postId);


            if (deleted)
                return NoContent();

            return NotFound();
        }

        [HttpPost(APIRoute.Post.MultipleCreate)]
        public async Task<IActionResult> MultipleCreate([FromBody] List<PostRequest> postRequests)
        {
            foreach (var postRequest in postRequests)
            {
                var post = new Post() { Id = Guid.NewGuid(), Name = postRequest.Name, UserId = HttpContext.GetUserId() };
                await postService.CreatePost(post);
            }
            return Ok();
        }
    }
}