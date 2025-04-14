using App.Dto.UserDtos;
using App.Services.Services.ApiServices.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace App.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthApiService _authApiService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AuthController(AuthApiService authApiService, IWebHostEnvironment webHostEnvironment)
        {
            _authApiService = authApiService;
            _webHostEnvironment = webHostEnvironment;
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

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, id),
        new Claim(ClaimTypes.Name, username),
        new Claim(ClaimTypes.Email, email)
    };

            claims.AddRange(jwtToken.Claims.Where(c => c.Type == ClaimTypes.Role));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

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
        public async Task<IActionResult> Register(RegisterDtoMvc dto)
        {
            if (!ModelState.IsValid) return View(dto);

            string? imagePath = null;

            if (dto.ImagePath != null && dto.ImagePath.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(dto.ImagePath.FileName);
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "userImages");

                Directory.CreateDirectory(folderPath);
                var fullPath = Path.Combine(folderPath, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);
                await dto.ImagePath.CopyToAsync(stream);

                imagePath = "/uploads/userImages/" + fileName;
            }
            var registerDto = new RegisterDto
            {
                UserFullName = dto.UserFullName,
                UserName = dto.UserName,
                Email = dto.Email,
                Password = dto.Password,
                ConfirmPassword = dto.ConfirmPassword,
                PhoneNumber = dto.PhoneNumber,
                UserProfilePictureUrl = imagePath
            };



            var isSuccess = await _authApiService.RegisterAsync(registerDto);
            if (isSuccess) return RedirectToAction("Login");

            ModelState.AddModelError("", "Kayıt başarısız. Tekrar deneyin.");
            return View(dto);
        }
        [Authorize(Roles ="Admin,Member,Moderator")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await _authApiService.LogoutAsync(); 

            return RedirectToAction("Login");
        }

    }
}
