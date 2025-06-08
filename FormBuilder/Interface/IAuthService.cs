using FormBuilder.DTO;

namespace FormBuilder.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterViewModel model);
        Task<bool> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
    }

}
