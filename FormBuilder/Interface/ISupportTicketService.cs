using FormBuilder.Models;
using System.Threading.Tasks;

namespace FormBuilder.Services
{
    public interface ISupportTicketService
    {
        Task<bool> UploadSupportTicketAsync(string summary, string priority, string pageUrl, string reportedBy);
    }

}
