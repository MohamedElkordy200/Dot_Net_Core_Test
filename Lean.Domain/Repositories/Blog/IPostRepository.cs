using Lean.Contracts.Blog.Dto;
using Lean.Contracts.Blog.SearchModels;
using Lean.Domain.DBEntities.Blog;

namespace Lean.Domain.Repositories.Blog
{
    public interface IPostRepository
    {
        Task<Post> GetPostByIdAsync(Guid postId);
        Task<IEnumerable<PostOutputDto>> GetAllPostsAsync(PostSm searchModel);
        Task<IEnumerable<Post>> GetMyPostsAsync(PostSm searchModel);
        Task InsertAsync(Post post);
        void Update(Post post);
        Task DeleteAsync(Guid postId);
    }
}
