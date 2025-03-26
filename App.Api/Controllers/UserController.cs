using App.Services.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("getUsers")]
        public async Task<IActionResult> GetUserList()
        {
            var users= await  _userService.GetUserListAsync();
            return Ok(users);
           
        }
    }
}
