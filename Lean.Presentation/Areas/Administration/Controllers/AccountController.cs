using Microsoft.AspNetCore.Mvc;
using Lean.Contracts.Administration;
using Lean.Services.Abstractions;

namespace Lean.Presentation.Areas.Administration.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[Action]")]
    public class AccountController : ControllerBase
    {
        private readonly IServicesManager _servicesManager;

        public AccountController(IServicesManager servicesManager)
        {
            _servicesManager = servicesManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegistrationInputDto userModel)
        {
            var result = await _servicesManager.IdentityService.RegisterAsync(userModel);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginInputDto userModel)
        {
            var result = await _servicesManager.IdentityService.LoginAsync(userModel);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            var result = await _servicesManager.IdentityService.LogoutAsync();
            return Ok(result);
        }
    }
}