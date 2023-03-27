using AutoMapper;
using Lean.Contracts.Blog.Dto;
using Lean.Contracts.Blog.SearchModels;
using Lean.Contracts.MessageModels;
using Lean.Domain.DBEntities.Blog;
using Lean.Domain.Exceptions.Blog;
using Lean.Domain.Repositories;
using Lean.Services.Abstractions.Blog;

namespace Lean.Services.Blog
{
    internal sealed class CommentService : BaseService, ICommentService
    {
        public CommentService(IMapper mapper, IRepositoriesManager repositoryManager) : base(mapper, repositoryManager)
        {
        }

        public async Task<MessageModel> AddAsync(CommentInputDto model)
        {
            await _repositoryManager.CommentRepository.InsertAsync(_mapper.Map<Comment>(model));
            return await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task<MessageModel> EditAsync(CommentUpdateInputDto model)
        {
            var comment = await _repositoryManager.CommentRepository.GetCommentByIdAsync(model.Id);
            if (comment is null)
                throw new PostNotFoundException();
            comment.Text = model.Text;
            _repositoryManager.CommentRepository.Update(comment);
            return await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task<MessageModel> Delete(Guid id)
        {
            await _repositoryManager.CommentRepository.DeleteAsync(id);
            return await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }


        public async Task<IEnumerable<CommentOutputDto>> GetPostCommentsAsync(CommentSm searchModel)
        {
            return (await _repositoryManager.CommentRepository.GetPostCommentsAsync(searchModel)).Select(a =>
                new CommentOutputDto
                {
                    Id = a.Id,
                    PostId = a.PostId,
                    Text = a.Text,
                    CreatedBy = a.CreatedBy,
                    CreatedAt = a.CreatedAt,
                }).ToList();
        }
    }
}