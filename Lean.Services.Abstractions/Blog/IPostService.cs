using Lean.Contracts.Blog.Dto;
using Lean.Contracts.Blog.SearchModels;
using Lean.Services.Abstractions._Common;

namespace Lean.Services.Abstractions.Blog
{
    public interface IPostService : IAddable<PostInputDto>, IEditable<PostInputDto>, IDeletable,
        IIdSearchable<PostOutputDto>
    {
        public Task<IEnumerable<PostOutputDto>> SearchAllPostsAsync(PostSm searchModel);
        public Task<IEnumerable<PostOutputDto>> SearchMyPostsAsync(PostSm searchModel);
    }
}