using FormBuilder.Interface;
using FormBuilder.Interfaces;
using FormBuilder.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("admin")]
public class AdminController : Controller
{
    private readonly IAdminService _adminService;
    private readonly IAuthService _authService;
    public AdminController(IAdminService adminService, IAuthService authService)
    {
        _adminService = adminService;
        _authService = authService;
    }
    [HttpGet("dashboard")]
    public IActionResult Dashboard()
    {
        return View();
    }

    [HttpGet("users")]
    public async Task<IActionResult> User()
    {
        var users = await _authService.GetLoggedInUserAsync();
        return View(users);
    }

    [HttpPost("add-admins")]
    public async Task<IActionResult> AddAdmins([FromBody] List<string> userIds)
    {
        await _adminService.AddAdminsAsync(userIds);
        return Ok(new { message = "Users added as admins successfully." });
    }

    [HttpPost("remove-admins")]
    public async Task<IActionResult> RemoveAdmins([FromBody] List<string> userIds)
    {
        await _adminService.RemoveAdminsAsync(userIds);
        return Ok(new { message = "Users removed from admins successfully." });
    }

    [HttpPost("block-users")]
    public async Task<IActionResult> BlockUsers([FromBody] List<string> userIds)
    {
        await _adminService.BlockUsersAsync(userIds);
        return Ok(new { message = "Users blocked successfully." });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BlockUser(string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest();

    await _adminService.BlockUsersAsync(new List<string> { id });



        return RedirectToAction("User","Admin", new { id });
    }


    [HttpPost("unblock-users")]
    public async Task<IActionResult> UnblockUsers([FromBody] List<string> userIds)
    {
        await _adminService.UnblockUsersAsync(userIds);
        return Ok(new { message = "Users unblocked successfully." });
    }

    [HttpPost("delete-users")]
    public async Task<IActionResult> DeleteUsers([FromBody] List<string> userIds)
    {
        await _adminService.DeleteUsersAsync(userIds);
        return Ok(new { message = "Users deleted successfully." });
    }
}
