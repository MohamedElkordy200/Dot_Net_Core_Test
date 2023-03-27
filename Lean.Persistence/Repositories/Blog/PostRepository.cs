using Lean.Contracts.Administration;
using Lean.Contracts.Blog.Dto;
using Lean.Contracts.Blog.SearchModels;
using Lean.Domain.DBEntities.Blog;
using Lean.Domain.Exceptions.Blog;
using Lean.Domain.Repositories.Blog;
using Microsoft.EntityFrameworkCore;

namespace Lean.Persistence.Repositories.Blog
{
    internal sealed class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PostRepository(ApplicationDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Post> GetPostByIdAsync(Guid postId)
        {
            return await _dbContext.Posts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == postId);
        }

        public async Task<IEnumerable<PostOutputDto>> GetAllPostsAsync(PostSm searchModel)
        {
            return await (from post in _dbContext.Posts
                    join user in _dbContext.Users on post.CreatedBy equals user.Id
                    where
                        searchModel.Description == null || post.Description.Contains(searchModel.Description) &&
                        searchModel.Title == null || post.Title.Contains(searchModel.Title)
                    select new PostOutputDto
                    {
                        Id = post.Id,
                        Title = post.Title,
                        Description = post.Description,
                        CreatedAt = post.CreatedAt,
                        CreatedBy = user.FirstName + " " + user.LastName,
                    }
                ).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetMyPostsAsync(PostSm searchModel)
        {
            return await _dbContext.Posts.Where(a =>
                    a.CreatedBy == UserIdentity.UserId &&
                    searchModel.Description == null || a.Description.Contains(searchModel.Description) &&
                    searchModel.Title == null || a.Title.Contains(searchModel.Title)
                )
                .ToListAsync();
        }

        public async Task InsertAsync(Post post)
        {
            await _dbContext.AddAsync(post);
        }

        public void Update(Post post)
        {
            _dbContext.Update(post);
        }

        public async Task DeleteAsync(Guid postId)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(x => x.Id == postId);
            if (post == null)
            {
                throw new PostNotFoundException();
            }

            _dbContext.Remove(post);
        }
    }
}