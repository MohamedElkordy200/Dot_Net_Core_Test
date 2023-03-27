using Lean.Contracts.Blog.Dto;
using Lean.Contracts.Blog.SearchModels;
using Lean.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lean.Presentation.Areas.Blog.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]/[Action]")]
    public class PostsController : ControllerBase
    {
        private readonly IServicesManager _servicesManager;

        public PostsController(IServicesManager servicesManager)
        {
            _servicesManager = servicesManager;
        }

        #region Crud Operations

        [HttpPost]
        public async Task<IActionResult> Add(PostInputDto postDto)
        {
            var response = await _servicesManager.PostService.AddAsync(postDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(PostInputDto postDto)
        {
            var response = await _servicesManager.PostService.EditAsync(postDto);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid postId)
        {
            var response = await _servicesManager.PostService.Delete(postId);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid commentId)
        {
            var response = await _servicesManager.PostService.GetByIdAsync(commentId);
            
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> SearchAllPosts(PostSm searchModel)
        {
            var response = await _servicesManager.PostService.SearchAllPostsAsync(searchModel);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> SearchMyPosts(PostSm searchModel)
        {
            var response = await _servicesManager.PostService.SearchMyPostsAsync(searchModel);

            return Ok(response);
        }

        #endregion
    }
}