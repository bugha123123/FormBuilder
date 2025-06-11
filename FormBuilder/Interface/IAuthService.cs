using FormBuilder.DTO;
using FormBuilder.Models;

namespace FormBuilder.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterViewModel model);
        Task<bool> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
        Task<User?> GetLoggedInUserAsync();

    }

}
