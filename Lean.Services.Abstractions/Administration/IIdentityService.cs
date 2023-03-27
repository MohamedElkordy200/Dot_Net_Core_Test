
using Lean.Contracts.Administration;
using Lean.Contracts.MessageModels;

namespace Lean.Services.Abstractions.Administration
{
    public interface IIdentityService
    {
        Task<MessageModel> RegisterAsync(UserRegistrationInputDto userModel);
        Task<MessageModel> LoginAsync(UserLoginInputDto userModel);
        Task<MessageModel> LogoutAsync();
    }
}
