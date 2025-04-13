using Microsoft.AspNetCore.Mvc;

namespace App.Web.Controllers
{
    public class GaleryController : Controller
    {
        public IActionResult Gallery()
        {
            var imageFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/comments");
            var imageUrls = Directory.GetFiles(imageFolder)
                                     .Select(file => "/uploads/comments/" + Path.GetFileName(file))
                                     .ToList();

            return View(imageUrls);
        }
    }
}
