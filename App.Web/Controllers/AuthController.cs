using App.Dto.UserDtos;
using App.Services.Services.ApiServices.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthApiService _authApiService;

        public AuthController(AuthApiService authApiService)
        {
            _authApiService = authApiService;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return View(loginDto);
            var isLoggedIn=await _authApiService.LoginAsync(loginDto);
            if (!isLoggedIn)
            {
                ModelState.AddModelError("", "Geçersiz e-posta veya şifre.");
                return View(loginDto);
            }
            return RedirectToAction("Index", "MainPage");

        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register( RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return View(registerDto);
            var isRegistered = await _authApiService.RegisterAsync(registerDto);

            if (isRegistered)
            {
               return RedirectToAction("Login");
            }
            ModelState.AddModelError("", "Kayıt başarısız. Lütfen tekrar deneyin.");
            return View(registerDto);
        }
        public async Task<IActionResult> Logout()
        {
            var isLoggedOut = await _authApiService.LogoutAsync();

            if (isLoggedOut)
            {
                return RedirectToAction("Login");
            }

            ModelState.AddModelError("", "Çıkış işlemi başarısız oldu.");
            return View();
        }

    }
}
