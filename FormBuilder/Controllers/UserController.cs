using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace FormBuilder.Controllers
{
    [Authorize] 
    public class UserController : Controller
    {
   
        public IActionResult Dashboard()
        {
          
            return View();
        }
    }
}
