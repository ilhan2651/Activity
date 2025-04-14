using App.Dto.Role;
using App.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

     
        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return Ok(roles);
        }

       
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewDto dto)
        {
            var result = await _roleManager.CreateAsync(new AppRole { Name = dto.Name });
            if (result.Succeeded)
                return Ok();

            return BadRequest(result.Errors);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole(RoleUpdateDto dto)
        {
            var role = await _roleManager.FindByIdAsync(dto.Id.ToString());
            if (role == null)
                return NotFound();

            role.Name = dto.Name;
            var result = await _roleManager.UpdateAsync(role);

            return result.Succeeded ? Ok() : BadRequest(result.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
                return NotFound();

            var result = await _roleManager.DeleteAsync(role);
            return result.Succeeded ? Ok() : BadRequest(result.Errors);
        }

        [HttpGet("user-roles/{userId}")]
        public async Task<IActionResult> GetUserRoles(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var roles = await _userManager.GetRolesAsync(user);
            return Ok(roles);
        }

        [HttpPost("assign-roles/{userId}")]
        public async Task<IActionResult> AssignRoles(int userId, List<RoleAssignDto> model)
        {
            Console.WriteLine($"⚙️ [API] AssignRoles => userId={userId}, modelCount={model.Count}");
            foreach (var item in model)
            {
                Console.WriteLine($"    - (API Param) {item.Name}, Exists={item.Exists}");
            }

            var user = await _userManager.FindByIdAsync(userId.ToString());

            foreach (var item in model)
            {
                if (item.Exists)
                {
                    
                    var result = await _userManager.AddToRoleAsync(user, item.Name);

                    if (!result.Succeeded)
                    {
                        Console.WriteLine($"  ❌ AddToRoleAsync HATA => {item.Name} => "
                                          + string.Join(',', result.Errors.Select(e => e.Description)));
                    }
                }
                else
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, item.Name);

                    if (!result.Succeeded)
                    {
                        Console.WriteLine($"  ❌ RemoveFromRoleAsync HATA => {item.Name} => "
                                          + string.Join(',', result.Errors.Select(e => e.Description)));
                    }
                }
            }
            return Ok();
        }

        [HttpGet("assign-roles-get/{userId}")]
        public async Task<IActionResult> GetAssignableRoles(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);

            var result = roles.Select(role => new RoleAssignDto
            {
                RoleId = role.Id,
                Name = role.Name,
                Exists = userRoles.Contains(role.Name)
            }).ToList();

            return Ok(result);
        }

        [HttpGet("get-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users
                .Select(x => new AppUserDto
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email
                })
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
                return NotFound();

            var dto = new RoleUpdateDto
            {
                Id = role.Id,
                Name = role.Name
            };

            return Ok(dto);
        }
    }
}
