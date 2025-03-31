using App.Dto.CommentDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace App.Web.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly HttpClient _httpClient;

        public CommentController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CreateCommentDtoMvc dto)
        {
            var token = Request.Cookies["jwt"];
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var form = new MultipartFormDataContent();
            form.Add(new StringContent(dto.EventId.ToString()), "EventId");
            form.Add(new StringContent(dto.Content), "Content");

            // Dosya varsa, yükleme işlemi
            if (dto.CommentImage != null)
            {
                var stream = dto.CommentImage.OpenReadStream();
                form.Add(new StreamContent(stream), "CommentImage", dto.CommentImage.FileName);
            }

            var response = await _httpClient.PostAsync("https://localhost:44344/api/Comment/AddComment", form);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Yorum gönderilemedi.";
                return RedirectToAction("ReadAll", "Event", new { id = dto.EventId }); // Hata durumunda geri dönüyoruz
            }

            TempData["Success"] = "Yorum başarıyla eklendi.";
            return RedirectToAction("ReadAll", "Event", new { id = dto.EventId });
        }

    }
}
