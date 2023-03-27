using AutoMapper;
using Lean.Domain.DBEntities.Administration;
using Lean.Domain.Repositories;
using Lean.Services.Abstractions;
using Lean.Services.Abstractions.Administration;
using Lean.Services.Abstractions.Blog;
using Lean.Services.Administration;
using Lean.Services.Blog;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Lean.Services
{
    public sealed class ServicesManager : IServicesManager
    {
        private readonly Lazy<IIdentityService> _identityService;
        private readonly Lazy<IPostService> _lazyPostService;
        private readonly Lazy<ICommentService> _lazyCommentService;

        public ServicesManager(IMapper mapper, IRepositoriesManager repositoryManager, IServiceScopeFactory serviceScopeFactory)
        {
    
            _identityService = new Lazy<IIdentityService>(() => new IdentityService(mapper, serviceScopeFactory));
            _lazyPostService = new Lazy<IPostService>(() => new PostService(mapper, repositoryManager));
            _lazyCommentService = new Lazy<ICommentService>(() => new CommentService(mapper, repositoryManager));
        }

        #region Blog

        public IIdentityService IdentityService => _identityService.Value;
        public IPostService PostService => _lazyPostService.Value;
        public ICommentService CommentService => _lazyCommentService.Value;

        #endregion
    }
}