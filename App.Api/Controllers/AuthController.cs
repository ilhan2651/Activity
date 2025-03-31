using App.Dto.UserDtos;
using App.Entities;
using App.Services.Services.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<AppUser> _userManager;

        public AuthController(IAuthService authService,UserManager<AppUser> userManager)
        {
            _authService = authService;
            _userManager= userManager;  
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var token = await _authService.LoginUser(model);
            if (token == null)
                return Unauthorized(new { message = "Geçersiz Giriş Bilgileri" });

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return Unauthorized(new { message = "Kullanıcı bulunamadı" });

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1)
            };

            Response.Cookies.Append("jwt", token, cookieOptions); 

            return Ok(new
            {
                token,
                user = new
                {
                    id = user.Id,
                    email = user.Email,
                    username = user.UserName
                }
            });
        }


        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {

            // JWT Token çerezini sil
            Response.Cookies.Delete("JWTToken");
            Response.Cookies.Delete(".AspNetCore.Identity.Application");
            // Kullanıcının kimlik bilgisini sıfırla
            HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

            return Ok(new { message = "Çıkış yapıldı" });
        }






        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId=User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user =await _authService.GetUserById(userId);
            if (user == null) return NotFound(new {message="Kullanıcı bulunamadı"});
            return Ok(user);
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterDto registerDto)
        {
            var result= await _authService.Register(registerDto);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok(new { message = "Kullanıcı başarıyla oluşturuldu." });
        }
     
    }
}
// git add .
//git commit -m "Açıklayıcı bir mesaj"
//git push origin main