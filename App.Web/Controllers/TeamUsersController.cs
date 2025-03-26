using App.Services.Services.ApiServices.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Controllers
{
    public class TeamUsersController : Controller
    {
        private readonly UserApiService _userApiService;

        public TeamUsersController(UserApiService userApiService)
        {
            _userApiService = userApiService;
        }

        public async Task<IActionResult> Index()
        {
            var users= await _userApiService.GetUsersAsync();
            return View(users);
        }
    }
}
