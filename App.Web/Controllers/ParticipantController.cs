using App.Dto.EventParticipantDtos;
using App.Services.Services.ApiServices.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Net;
using System.Security.Claims;

namespace App.Web.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class ParticipantController : Controller
    {
        private readonly EventParticipantApiService _epApiService;
        public ParticipantController(EventParticipantApiService epApiService)
        {
            _epApiService= epApiService;
        }
        [HttpGet]
     
        public async Task<IActionResult> AddParticipant(int eventId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var dto = new JoinEventDto
            {
                EventId = eventId,
                UserId = int.Parse(userId)
            };

            try
            {
                await _epApiService.AddParticipant(dto); // ❗ Bu burada throw eder

                TempData["Success"] = "Etkinliğe katılacağınızı belirttiniz..";
            }
            catch (ApiException ex)
            {
                if (ex.StatusCode == HttpStatusCode.BadRequest)
                {
                    var content = await ex.GetContentAsAsync<Dictionary<string, string>>();
                    if (content.TryGetValue("message", out var message) && message.Contains("zaten katıldınız"))
                    {
                        TempData["Info"] = "Zaten bu etkinliğe katıldınız.";
                    }
                    else
                    {
                        TempData["Error"] = "Katılım sırasında bir hata oluştu.";
                    }
                }
                else
                {
                    TempData["Error"] = "Sunucu hatası: " + ex.StatusCode;
                }
            }


            return RedirectToAction("Index", "MainPage");
        }
        public async Task<IActionResult> DeleteParticipant(int id)
        {
            var result = await _epApiService.DeleteParticipant(id);

            if (result)
                TempData["Success"] = "Katılım başarıyla kaldırıldı.";
            else
                TempData["Error"] = "Katılım kaldırılamadı.";

            return RedirectToAction("Index", "MainPage");
        }


    }
}
