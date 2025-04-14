using App.Dto.Role;
using App.Services.Services.ApiServices.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Web.Areas.Moderator.Controllers
{
    [Area("Moderator")]
    [Authorize(Roles = "Admin")]
    public class ModeratorRoleController : Controller
    {
        private readonly RoleApiService _roleApiService;

        public ModeratorRoleController(RoleApiService roleApiService)
        {
            _roleApiService = roleApiService;
        }

      
        public async Task<IActionResult> Index()
        {
            var roles = await _roleApiService.GetAllRolesAsync();
            return View(roles); 
        }

        [HttpGet]
        public IActionResult AddRole() => View();

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewDto dto)
        {
            var success = await _roleApiService.AddRoleAsync(dto);
            if (success)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Rol eklenirken hata oluştu.");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateRole(int id)
        {
            var role = await _roleApiService.GetRoleByIdAsync(id); 
            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(RoleUpdateDto dto)
        {
            var success = await _roleApiService.UpdateRoleAsync(dto);
            if (success)
                return RedirectToAction("Index");

            return View(dto);
        }

        public async Task<IActionResult> DeleteRole(int id)
        {
            await _roleApiService.DeleteRoleAsync(id);
            return RedirectToAction("Index");
        }

        
        [HttpGet]
        public async Task<IActionResult> UserRoleList()
        {
            var users = await _roleApiService.GetAllUsersAsync(); 
            return View(users);
        }

        
        [HttpGet]
        public async Task<IActionResult> AssignRole(int id)
        {
           
            var roles = await _roleApiService.GetAssignableRolesAsync(id);
            ViewBag.UserId = id;
            return View(roles); 
        }


        [HttpPost]
        public async Task<IActionResult> AssignRole(List<RoleAssignDto> model, int userId)
        {
            Console.WriteLine($"🟢 [MVC] POST AssignRole => userId = {userId}");
            foreach (var item in model)
            {
                Console.WriteLine($"    * (MVC) {item.Name} => Exists = {item.Exists}");
            }

            await _roleApiService.AssignRolesAsync(userId, model);
            return RedirectToAction("UserRoleList");
        }
    }
}
