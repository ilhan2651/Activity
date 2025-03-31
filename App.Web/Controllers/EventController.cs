using App.Services.Services.ApiServices.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly EventApiService _eventApiService;

        public EventController(EventApiService eventApiService)
        {
            _eventApiService = eventApiService;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _eventApiService.GetEventsAllWeek();
            return View(events);
        }
        public async Task<IActionResult> ReadAll(int id)
        {
            var evnt = await _eventApiService.GetEventById(id);
            if (evnt == null)
            {
                TempData["Error"] = "Etkinlik bulunamadı.";
                return RedirectToAction("Index", "Event");
            }

            return View(evnt);
        }



    }
}
