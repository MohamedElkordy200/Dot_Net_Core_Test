using Lean.Contracts.Administration;
using Lean.Contracts.Blog.Dto;
using Lean.Contracts.Blog.SearchModels;
using Lean.Domain.DBEntities.Blog;
using Lean.Domain.Exceptions.Blog;
using Lean.Domain.Repositories.Blog;
using Microsoft.EntityFrameworkCore;

namespace Lean.Persistence.Repositories.Blog
{
    internal sealed class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CommentRepository(ApplicationDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Comment> GetCommentByIdAsync(Guid commentId)
        {
            return await _dbContext.Comments.AsNoTracking().FirstOrDefaultAsync(x => x.Id == commentId);
        }

        public async Task<IEnumerable<CommentOutputDto>> GetPostCommentsAsync(CommentSm searchModel)
        {
            var result = await (from comment in _dbContext.Comments
                    join post in _dbContext.Posts on comment.PostId equals post.Id
                    join user in _dbContext.Users on comment.CreatedBy equals user.Id
                    where
                        comment.PostId == searchModel.PostId &&
                        searchModel.Text == null || comment.Text.Contains(searchModel.Text) &&
                        searchModel.PostDescription == null || post.Description.Contains(searchModel.PostDescription)
                    select new CommentOutputDto
                    {
                        Id = comment.Id,
                        PostId = comment.Id,
                        Text = comment.Text,
                        CreatedAt = comment.CreatedAt,
                        CreatedBy = user.FirstName +" "+ user.LastName,
                    }
                ).ToListAsync();
            return result;
        }

        public async Task InsertAsync(Comment comment)
        {
            await _dbContext.AddAsync(comment);
        }

        public void Update(Comment comment)
        {
            _dbContext.Update(comment);
        }

        public async Task DeleteAsync(Guid commentId)
        {
            var post = await _dbContext.Comments.FirstOrDefaultAsync(x => x.Id == commentId);
            if (post == null)
            {
                throw new CommentNotFoundException();
            }

            _dbContext.Remove(post);
        }
    }
}