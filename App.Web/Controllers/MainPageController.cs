using App.Services.Services.ApiServices.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Controllers
{
    public class MainPageController : Controller
    {
        private readonly EventApiService _eventApiService;

        public MainPageController(EventApiService eventApiService)
        {
            _eventApiService = eventApiService;
        }

        public async Task<IActionResult> Index()
        {
            var activity = await _eventApiService.GetWeeklyActivitiesAsync();

            if (activity == null)
            {
                ViewBag.ErrorMessage = "Bu hafta için etkinlik bulunamadı.";
                return View();
            }

            return View(activity);
        }

    }
}
