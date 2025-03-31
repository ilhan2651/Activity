using App.Dto.ContactDto;
using App.Services.Services.ApiServices.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactApiService _contactApiService;
        public ContactController(ContactApiService contactApiService)
        {
            _contactApiService = contactApiService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(CreateContactDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Formda eksik veya hatalı alanlar var.";
                return View(dto);
            }
            var result = await _contactApiService.PostContact(dto);
            if (result !=null)
            {

                TempData["SuccessMessage"] = result;
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Mesaj gönderilirken bir hata oluştu.";
            return View(dto);
        }
    }
}
