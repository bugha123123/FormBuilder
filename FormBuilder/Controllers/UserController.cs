using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FormBuilder.Interface;

namespace FormBuilder.Controllers
{
    [Authorize] 
    public class UserController : Controller
    {

        private readonly IUserService _userService;



        public IActionResult Dashboard()
        {
          
            return View();
        }
        public async Task<IActionResult> AutoCompleteUsers(string keyWord)
        {
            var users = await _userService.SearchUsersAsync(keyWord);

            var result = users.Select(u => new
            {
                u.Username,
                u.Email
            });

            return Json(result);
        }


    }
}
