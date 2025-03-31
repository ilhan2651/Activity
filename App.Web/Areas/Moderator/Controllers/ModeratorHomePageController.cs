using Microsoft.AspNetCore.Mvc;

namespace App.Web.Areas.Moderator.Controllers
{
    [Area("Moderator")]
    public class ModeratorHomePageController : Controller
    {
        public IActionResult HomePage()
        {
            return View();
        }
    }
}
