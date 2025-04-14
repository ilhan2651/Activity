using App.Services.Services.ApiServices.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Areas.Moderator.Controllers
{
    [Area("Moderator")]
    [Authorize(Roles ="Admin,Moderator")]
    public class ModeratorCommentController : Controller
    {
        private readonly CommentApiService _commentApiService;
        public ModeratorCommentController(CommentApiService commentApiService)
        {
            _commentApiService = commentApiService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var token = HttpContext.Request.Cookies["JWTToken"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }
            var comments = await _commentApiService.GetAllComents(token);
            if (comments == null)
            {
                TempData["Error"] = "Yorumlar alınamadı.";
                return RedirectToAction("Index", "ModeratorEvent");
            }
            return View(comments);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var token = HttpContext.Request.Cookies["JWTToken"];

            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Yetki bulunamadı.";
                return RedirectToAction("GetAllComments");
            }

            var result = await _commentApiService.DeleteComment(id, token);

            if (!result)
            {
                TempData["Error"] = "Yorum silinemedi.";
            }
            TempData["Success"] = "Yorum silindi.";
            return RedirectToAction("GetAllComments");
        }
    }
}
