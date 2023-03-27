using AutoMapper;
using Lean.Domain.Repositories;
using Lean.Domain.Repositories.Blog;
using Lean.Persistence.Repositories.Blog;
using Microsoft.Extensions.DependencyInjection;

namespace Lean.Persistence.Repositories
{
    public sealed class RepositoriesManager : IRepositoriesManager
    {
        private readonly Lazy<IPostRepository> _lazyPostRepository;
        private readonly Lazy<ICommentRepository> _lazyCommentRepository;
        private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;

        public RepositoriesManager(ApplicationDbContext deContext)
        {

            _lazyPostRepository = new Lazy<IPostRepository>(() => new PostRepository(deContext));
            _lazyCommentRepository = new Lazy<ICommentRepository>(() => new CommentRepository(deContext));
            _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(deContext));
        }

        #region Blog

        public IPostRepository PostRepository => _lazyPostRepository.Value;
        public ICommentRepository CommentRepository => _lazyCommentRepository.Value;
        public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;

        #endregion
    }
}