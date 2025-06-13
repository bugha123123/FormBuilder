using FormBuilder.DTO;
using FormBuilder.Models;

namespace FormBuilder.Interface
{
    public interface IUserService
    {
        Task<List<UserDTO>> SearchUsersAsync(string term);

        Task<List<User>> GetUsers(int Id);


    }
}
