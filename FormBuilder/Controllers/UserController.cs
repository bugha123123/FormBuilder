using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FormBuilder.Interface;

namespace FormBuilder.Controllers
{
    [Authorize] 
    public class UserController : Controller
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Dashboard()
        {
          
            return View();
        }
        // UserController.cs
        public async Task<IActionResult> AutoCompleteUsers(string keyword)
        {
            var users = await _userService.SearchUsersAsync(keyword);
            return Json(users.Select(u => new { u.Username, u.Email }));
        }



    }
}
