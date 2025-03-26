using Microsoft.AspNetCore.Mvc;

namespace App.Web.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
