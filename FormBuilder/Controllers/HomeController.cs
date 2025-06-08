using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FormBuilder.Controllers;

public class HomeController : Controller
{


    public IActionResult Index()
    {
        return View();
    }

    
}
 