using AutoMapper;
using Lean.Contracts.Administration;
using Lean.Contracts.Blog.Dto;
using Lean.Contracts.Blog.SearchModels;
using Lean.Contracts.MessageModels;
using Lean.Domain.Repositories;
using Lean.Services.Abstractions.Blog;
using Lean.Domain.DBEntities.Blog;
using Lean.Domain.Exceptions.Blog;

namespace Lean.Services.Blog
{
    internal sealed class PostService : BaseService, IPostService
    {
        public PostService(IMapper mapper, IRepositoriesManager repositoryManager) : base(mapper, repositoryManager)
        {
        }

        public async Task<MessageModel> AddAsync(PostInputDto model)
        {
            await _repositoryManager.PostRepository.InsertAsync(_mapper.Map<Post>(model));
            return await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task<MessageModel> EditAsync(PostInputDto model)
        {
            if (model.Id == null)
                throw new PostNotFoundException();
            var post = await _repositoryManager.PostRepository.GetPostByIdAsync(model.Id.Value);
            if (post is null)
                throw new PostNotFoundException();

            _repositoryManager.PostRepository.Update(_mapper.Map<Post>(model));

            return await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task<MessageModel> Delete(Guid id)
        {
            await _repositoryManager.PostRepository.DeleteAsync(id);
            return await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task<PostOutputDto> GetByIdAsync(Guid id)
        {
            var post = (await _repositoryManager.PostRepository.GetPostByIdAsync(id));

            return  _mapper.Map<PostOutputDto>(post);
        }

        public async Task<IEnumerable<PostOutputDto>> SearchAllPostsAsync(PostSm searchModel)
        {
            return (await _repositoryManager.PostRepository.GetAllPostsAsync(searchModel)).ToList() ;
        }
        public async Task<IEnumerable<PostOutputDto>> SearchMyPostsAsync(PostSm searchModel)
        {
            return (await _repositoryManager.PostRepository.GetMyPostsAsync(searchModel)).Select(a => new PostOutputDto
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                CreatedAt = a.CreatedAt,
                CreatedBy = UserIdentity.UserName
            }).ToList();
        }
    }
}