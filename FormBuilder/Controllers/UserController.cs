using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FormBuilder.Interface;
using FormBuilder.Service;
using Microsoft.AspNetCore.Identity;

namespace FormBuilder.Controllers
{
    [Authorize] 
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        private readonly ISalesForceService _salesforceService;
        public UserController(IUserService userService, ISalesForceService salesforceService)
        {
            _userService = userService;
            _salesforceService = salesforceService;
        }

        public IActionResult Dashboard()
        {
          
            return View();
        }
        
        public async Task<IActionResult> AutoCompleteUsers(string keyword)
        {
            var users = await _userService.SearchUsersAsync(keyword);
            return Json(users.Select(u => new { u.Username, u.Email }));
        }




        [HttpPost]
        public async Task<IActionResult> Sync(string companyName, string phoneNumber, string useCase)
        {
            if (string.IsNullOrWhiteSpace(companyName))
            {
                TempData["Message"] = "Company Name is required.";
                return RedirectToAction("Dashboard", "User");
            }

           

            var (success, error) = await _salesforceService.CreateSalesforceAccountAndContact(companyName, phoneNumber, useCase);

            TempData["Message"] = success ? "Successfully synced data with Salesforce!" : $"Salesforce sync failed: {error}";
            return RedirectToAction("Dashboard", "User");
        }

    }
}
