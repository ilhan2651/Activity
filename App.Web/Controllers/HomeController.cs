using App.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace App.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Unauthorized()
        {
            return View(); 
        }
    }
}
