// Services/SupportTicketService.cs
using FormBuilder.Interface;
using FormBuilder.Services;
using System.Text.Json;
using System.Threading.Tasks;

public class SupportTicketService : ISupportTicketService
{
    private readonly IDropboxService _dropboxService;

    public SupportTicketService(IDropboxService dropboxService)
    {
        _dropboxService = dropboxService;
    }

    public async Task<bool> UploadSupportTicketAsync(string summary, string priority, string pageUrl, string reportedBy)
    {
        var ticketData = new
        {
            ReportedBy = reportedBy,
            Link = pageUrl,
            Priority = priority,
            Summary = summary
        };

        string jsonContent = JsonSerializer.Serialize(ticketData, new JsonSerializerOptions { WriteIndented = true });
        string fileName = $"support-ticket-{System.DateTime.UtcNow:yyyyMMddHHmmss}.json";

        return await _dropboxService.UploadJsonFileAsync(fileName, jsonContent);
    }
}
