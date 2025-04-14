using App.Dto.CommentDto;
using App.Services.Services.ApiServices.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace App.Web.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly CommentApiService _commentApiService;
        private readonly HttpClient _httpClient;

        public CommentController(HttpClient httpClient, CommentApiService commentApiService)
        {
            _httpClient = httpClient;
            _commentApiService = commentApiService;
        }

        
        [HttpPost]
        public async Task<IActionResult> AddComment(CreateCommentDtoMvc dto)
        {
            var token = Request.Cookies["JWTToken"];
            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "Token bulunamadı. Lütfen tekrar giriş yapın.";
                return RedirectToAction("ReadAll", "Event", new { id = dto.EventId });
            }

            // DTO Dönüşümü
            var apiDto = new CreateCommentDto
            {
                EventId = dto.EventId,
                Content = dto.Content,
                CommentImage = dto.CommentImage
            };

            // Yorum gönderimi
            bool success = await _commentApiService.PostComment(apiDto, token);

            if (!success)
            {
                TempData["Error"] = "Yorum gönderilemedi.";
                return RedirectToAction("ReadAll", "Event", new { id = dto.EventId });
            }

            TempData["Success"] = "Yorum başarıyla eklendi.";
            return RedirectToAction("ReadAll", "Event", new { id = dto.EventId });
        }

    }

}

