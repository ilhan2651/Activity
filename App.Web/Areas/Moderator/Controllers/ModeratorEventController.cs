using App.Dto.EventDtos;
using App.Services.Services.ApiServices.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using App.Web.Areas.Moderator.Model.Event;

namespace App.Web.Areas.Moderator.Controllers
{
    [Area("Moderator")]
    public class ModeratorEventController : Controller
    {
        private readonly EventApiService _eventApiService;

        public ModeratorEventController(EventApiService eventApiService)
        {
            _eventApiService = eventApiService;
        }

        
        public async Task<IActionResult> Index()
        {
            var events = await _eventApiService.GetEventsAllWeek();
            return View(events);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreateEventViewModel
            {
                Date = DateTime.Now // datetime-local için default verir
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEventViewModel model)
        {
          
            if (!ModelState.IsValid)
            {
                Console.WriteLine("❌ ModelState INVALID");
                foreach (var kvp in ModelState)
                {
                    foreach (var error in kvp.Value.Errors)
                    {
                        Console.WriteLine($"🔴 {kvp.Key} => {error.ErrorMessage}");
                    }
                }

                return View(model);
            }

            if (model.EventImage == null || model.EventImage.Length == 0)
            {
                ModelState.AddModelError("EventImage", "Bir görsel yüklemelisiniz.");
                return View(model);
            }

            var fileName = $"{Guid.NewGuid()}_{model.EventImage.FileName}";
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/events", fileName);
            using var stream = new FileStream(imagePath, FileMode.Create);
            await model.EventImage.CopyToAsync(stream);

            // DTO oluştur
            var dto = new CreateEventDto
            {
                EventTitle = model.EventTitle,
                EventContent = model.EventContent,
                EventLocation = model.EventLocation,
                MaxParticipants = model.MaxParticipants,
                Date = model.Date,
                EventImageUrl = $"/uploads/events/{fileName}"
            };

            var response = await _eventApiService.CreateEventAsync(dto); // 1 yerine oturumdan kullanıcı ID'sini alabilirsiniz

            if (!response)
                return View(model);

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            var result = await _eventApiService.DeleteEventAsync(id);
            if (!result)
                TempData["Error"] = "Silme işlemi başarısız oldu.";
            return RedirectToAction("Index");
        }
    }
}
