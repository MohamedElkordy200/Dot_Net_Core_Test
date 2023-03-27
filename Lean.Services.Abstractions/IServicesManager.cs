using Lean.Services.Abstractions.Administration;
using Lean.Services.Abstractions.Blog;

namespace Lean.Services.Abstractions
{
    public interface IServicesManager
    {
        IIdentityService IdentityService { get; }
        IPostService PostService { get; }
        ICommentService CommentService { get; }
    }
}