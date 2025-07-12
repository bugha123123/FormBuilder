// Controllers/SupportTicketController.cs
using FormBuilder.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class SupportController : Controller
{
    private readonly ISupportTicketService _supportTicketService;

    public SupportController(ISupportTicketService supportTicketService)
    {
        _supportTicketService = supportTicketService;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubmitSupportTicket(string Summary, string Priority, string PageUrl)
    {
        var reportedBy = User.Identity?.Name ?? "Anonymous";

        bool result = await _supportTicketService.UploadSupportTicketAsync(Summary, Priority, PageUrl, reportedBy);

        if (!result)
        {
            TempData["Error"] = "Failed to submit support ticket.";
            return RedirectToAction("Index", "Home");
        }

        TempData["Message"] = "Support ticket submitted successfully!";
        return RedirectToAction("Index", "Home");
    }

}
