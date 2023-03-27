using Lean.Domain.Repositories.Blog;

namespace Lean.Domain.Repositories
{
    public interface IRepositoriesManager
    {
        IPostRepository PostRepository { get; }
        ICommentRepository CommentRepository { get; }
        IUnitOfWork UnitOfWork { get; }
    }
}