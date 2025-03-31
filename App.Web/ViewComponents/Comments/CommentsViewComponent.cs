using App.Services.Services.ApiServices.Concrete;
using Microsoft.AspNetCore.Mvc;

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
        ViewBag.CommentCount = comments?.Count ?? 0;
        return View(comments);
    }
}