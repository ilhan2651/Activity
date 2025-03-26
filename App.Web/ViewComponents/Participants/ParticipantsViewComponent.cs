using App.Dto.EventParticipantDtos;
using App.Services.Services.ApiServices.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.ViewComponents.Participants
{
    public class ParticipantsViewComponent : ViewComponent
    {
        private readonly EventParticipantApiService _eventParticipantApiService;

        public ParticipantsViewComponent(EventParticipantApiService eventParticipantApiService)
        {
            _eventParticipantApiService = eventParticipantApiService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int eventId)
        {
            var participants = await _eventParticipantApiService.GetParticipantsOfWeeklyEvents(eventId);

            if (participants == null )
            {
                ViewBag.ErrorMessage = "Bu etkinliğe henüz kimse katılmamış.";
                return View(new List<ParticipantListDto>()); 
            }

            return View(participants);
        }
    }
}
