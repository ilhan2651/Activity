using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using App.Dto.CommentDto;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IActionResult> AddComment(CreateCommentDto commentDto)
        {
            if (string.IsNullOrEmpty(commentDto.Content))
            {
                TempData["Error"] = "Yorum boş olamaz.";
                return RedirectToAction("ReadAll", "Event", new { id = commentDto.EventId });
            }

            // ✅ Kullanıcı kimliğini JWT içinden al
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                TempData["Error"] = "Kimlik doğrulama başarısız.";
                return RedirectToAction("ReadAll", "Event", new { id = commentDto.EventId });
            }

            if (!int.TryParse(userIdClaim, out int userId))
            {
                TempData["Error"] = "Geçersiz kullanıcı kimliği.";
                return RedirectToAction("ReadAll", "Event", new { id = commentDto.EventId });
            }

            // ✅ JWT Token'ı Cookie'den al (Eğer Cookie'de saklanıyorsa)
            var token = HttpContext.Request.Cookies["jwt"];

            // ✅ API'ye `POST` isteği yap
            var requestBody = new CreateCommentDto
            {
                EventId = commentDto.EventId,
                Content = commentDto.Content,
                UserId = userId
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.PostAsync("https://localhost:5001/api/CommentApi/AddComment", content);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Yorum eklenirken hata oluştu.";
                return RedirectToAction("ReadAll", "Event", new { id = commentDto.EventId });
            }

            TempData["Success"] = "Yorum başarıyla eklendi!";
            return RedirectToAction("ReadAll", "Event", new { id = commentDto.EventId });
        }
    }
}
