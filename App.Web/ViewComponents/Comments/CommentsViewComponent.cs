using App.Services.Services.ApiServices.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.Web.ViewComponents.Comments
{
    public class CommentsViewComponent : ViewComponent
    {
        private readonly CommentApiService _commentApiService;

        public CommentsViewComponent(CommentApiService commentApiService)
        {
            _commentApiService = commentApiService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int eventId)
        {
            var comments = await _commentApiService.GetCommentsByEventId(eventId);

            // ✅ Yorum sayısını ViewBag'e ekliyoruz
            ViewBag.CommentCount = comments.Count;

            return View(comments);
        }
    }
}
