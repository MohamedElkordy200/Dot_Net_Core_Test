using Lean.Contracts.Blog.Dto;
using Lean.Contracts.Blog.SearchModels;
using Lean.Domain.DBEntities.Blog;

namespace Lean.Domain.Repositories.Blog
{
    public interface ICommentRepository
    {
        Task<Comment> GetCommentByIdAsync(Guid commentId);
        Task<IEnumerable<CommentOutputDto>> GetPostCommentsAsync(CommentSm searchModel);
        Task InsertAsync(Comment comment);
        void Update(Comment comment);
        Task DeleteAsync(Guid commentId);
    }
}
