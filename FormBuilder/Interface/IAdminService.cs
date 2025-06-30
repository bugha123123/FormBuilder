using System.Collections.Generic;
using System.Threading.Tasks;
using FormBuilder.Models;

namespace FormBuilder.Interface
{
    public interface IAdminService
    {
        Task<List<User>> GetAllUsersAsync();
        Task AddAdminsAsync(List<string> userIds);
        Task RemoveAdminsAsync(List<string> userIds);
        Task BlockUsersAsync(List<string> userIds);
        Task UnblockUsersAsync(List<string> userIds);
        Task DeleteUsersAsync(List<string> userIds);

        Task<int> GetTemplateCountForUserAsync(string userId);
        Task<int> GetCommentCountForUserAsync(string userId);
        Task<int> GetLikeCountForUserAsync(string userId);
    }
}
