using Lean.Contracts.Blog.Dto;
using Lean.Contracts.Blog.SearchModels;
using Lean.Services.Abstractions._Common;

namespace Lean.Services.Abstractions.Blog
{
    public interface ICommentService : IAddable<CommentInputDto>, IEditable<CommentUpdateInputDto>, IDeletable
    {
        public Task<IEnumerable<CommentOutputDto>> GetPostCommentsAsync(CommentSm searchModel);
    }
}