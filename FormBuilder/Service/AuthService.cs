using FormBuilder.DTO;
using FormBuilder.Interfaces;
using FormBuilder.Models;
using Microsoft.AspNetCore.Identity;

namespace FormBuilder.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> RegisterAsync(RegisterViewModel model)
        {
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");

                await _signInManager.SignInAsync(user, isPersistent: false);
                return true;
            }

            return false;
        }


        public async Task<bool> LoginAsync(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                isPersistent:true,
                lockoutOnFailure: false);

            return result.Succeeded;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }


        public async Task<User?> GetLoggedInUserAsync()
        {
            if (_httpContextAccessor.HttpContext.User.Identity?.IsAuthenticated != true)
            {
                return null;
            }

            string? userName = _httpContextAccessor.HttpContext.User.Identity?.Name;

            if (userName == null)
            {

                return null;
            }

            var user = await _userManager.FindByNameAsync(userName);

            return user;
        }
    }
}
