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
    public class CommentsController : ControllerBase
    {
        private readonly IServicesManager _servicesManager;
        public CommentsController(IServicesManager servicesManager )
        {
            _servicesManager = servicesManager;
        }

        #region Crud Operations
        [HttpPost]
        public async Task<IActionResult> Add( CommentInputDto commentDto)
        {
            var response = await _servicesManager.CommentService.AddAsync(commentDto);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(CommentUpdateInputDto commentDto)
        {
            var response = await _servicesManager.CommentService.EditAsync(commentDto);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid commentId)
        {
            var response = await _servicesManager.CommentService.Delete(commentId);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> SearchByPost(CommentSm searchModel)
        {
            var response = await _servicesManager.CommentService.GetPostCommentsAsync(searchModel);

            return Ok(response);
        }
        #endregion
    }
}
