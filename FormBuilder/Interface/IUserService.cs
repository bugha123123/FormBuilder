using FormBuilder.DTO;

namespace FormBuilder.Interface
{
    public interface IUserService
    {
        Task<List<UserDTO>> SearchUsersAsync(string term);


    }
}
