using App.Dto.UserDtos;
using App.Services.Services.ApiServices.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthApiService _authApiService;

        public AuthController(AuthApiService authApiService)
        {
            _authApiService = authApiService;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return View(loginDto);

            var (success, token, id, username, email) = await _authApiService.LoginWithResultAsync(loginDto);

            if (!success)
            {
                ModelState.AddModelError("", "Geçersiz e-posta veya şifre.");
                return View(loginDto);
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, id),
        new Claim(ClaimTypes.Name, username),
        new Claim(ClaimTypes.Email, email)
    };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // ✅ TOKEN'I BURADA YAZIYORUZ
            Response.Cookies.Append("JWTToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });

            return RedirectToAction("Index", "MainPage");
        }



        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var isSuccess = await _authApiService.RegisterAsync(dto);
            if (isSuccess) return RedirectToAction("Login");

            ModelState.AddModelError("", "Kayıt başarısız. Tekrar deneyin.");
            return View(dto);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _authApiService.LogoutAsync(); 

            return RedirectToAction("Login");
        }

    }
}
