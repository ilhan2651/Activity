using Microsoft.AspNetCore.Mvc;

namespace App.Web.Areas.Moderator.Controllers
{
    public class ModeratorEventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
