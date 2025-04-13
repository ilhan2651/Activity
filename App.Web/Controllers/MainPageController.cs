using App.Services.Services.ApiServices.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace App.Web.Controllers
{
    public class MainPageController : Controller
    {
        private readonly EventApiService _eventApiService;
        private readonly EventParticipantApiService _eventParticipantApiService;

        public MainPageController(EventApiService eventApiService, EventParticipantApiService eventParticipantApiService)
        {
            _eventApiService = eventApiService;
            _eventParticipantApiService = eventParticipantApiService;
        }

        public async Task<IActionResult> Index()
        {
            var activity = await _eventApiService.GetWeeklyActivitiesAsync();

            if (activity == null)
            {
                ViewBag.ErrorMessage = "Bu hafta için etkinlik bulunamadı.";
                return View();
            }

            // Kullanıcı girişi yapılmış mı kontrolü
            if (User.Identity!.IsAuthenticated)
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var status = await _eventParticipantApiService.GetJoinStatus(activity.EventId, userId);

                ViewBag.IsJoined = status?.Joined ?? false;
                ViewBag.ParticipantId = status?.ParticipantId;
            }
            else
            {
                ViewBag.IsJoined = false;
            }

            return View(activity);
        }
    }
}
