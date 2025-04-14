using App.Entities;
using App.Services.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        public UserController(IUserService userService,UserManager<AppUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }
        [HttpGet("getUsers")]
        public async Task<IActionResult> GetUserList()
        {
            var users= await  _userService.GetUserListAsync();
            return Ok(users);
           
        }
        [HttpGet("GetAllEmails")]
        public async Task<IActionResult> GetAllEmails()
        {
            var users= await _userManager.Users.ToListAsync();
            var emails = users.Select(u => u.Email).ToList();
            return Ok(emails);
        }
    }
}
