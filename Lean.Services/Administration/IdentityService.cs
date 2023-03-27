using System.Security.Claims;
using AutoMapper;
using Lean.Contracts.Administration;
using Lean.Contracts.MessageModels;
using Lean.Domain.DBEntities.Administration;
using Lean.Domain.Exceptions.Administration;
using Lean.Services.Abstractions.Administration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace Lean.Services.Administration
{
    internal sealed class IdentityService : IIdentityService
    {
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public IdentityService(IMapper mapper, IServiceScopeFactory serviceScopeFactory)
        {
            _mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<MessageModel> RegisterAsync(UserRegistrationInputDto userModel)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var user = _mapper.Map<User>(userModel);
            var result = await userManager.CreateAsync(user, userModel.Password);

            if (result.Succeeded)
                return new SuccessMessage();

            throw new UserCreatedErrorBadRequestException(result.Errors.First().Description);
        }


        public async Task<MessageModel> LoginAsync(UserLoginInputDto userModel)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<User>>();
            var httpContextAccessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
            var user = await userManager.FindByNameAsync(userModel.UserName);
            if (user is not null && await userManager.CheckPasswordAsync(user, userModel.Password))
            {
                await signInManager.SignInAsync(user, false);

                //claims
                var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

                await httpContextAccessor.HttpContext.SignInAsync(IdentityConstants.ApplicationScheme,
                    new ClaimsPrincipal(identity));
                //set UserIdentity
                UserIdentity.UserId=user.Id;
                UserIdentity.UserName=user.UserName;
                return new SuccessMessage();
            }

            throw new UserLoginFailureBadRequestException();
        }

        public async Task<MessageModel> LogoutAsync()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<User>>();
            await signInManager.SignOutAsync();
            return new SuccessMessage();
        }
    }
}