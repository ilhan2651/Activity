using App.Dto.Role;
using System.Net.Http.Json;

namespace App.Services.Services.ApiServices.Concrete
{
    public class RoleApiService
    {
        private readonly HttpClient _client;

        public RoleApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<RoleIdNameDto>> GetAllRolesAsync()
        {
            return await _client.GetFromJsonAsync<List<RoleIdNameDto>>("api/role");
        }

        public async Task<bool> AddRoleAsync(RoleViewDto dto)
        {
            var response = await _client.PostAsJsonAsync("api/role", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<RoleUpdateDto> GetRoleByIdAsync(int id)
        {
            return await _client.GetFromJsonAsync<RoleUpdateDto>($"api/role/{id}");
        }

        public async Task<bool> UpdateRoleAsync(RoleUpdateDto dto)
        {
            var response = await _client.PutAsJsonAsync("api/role", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var response = await _client.DeleteAsync($"api/role/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<AppUserDto>> GetAllUsersAsync()
        {
            return await _client.GetFromJsonAsync<List<AppUserDto>>("api/role/get-users");
        }

        public async Task<List<RoleAssignDto>> GetAssignableRolesAsync(int userId)
        {
            return await _client.GetFromJsonAsync<List<RoleAssignDto>>($"api/role/assign-roles-get/{userId}");
        }

        public async Task AssignRolesAsync(int userId, List<RoleAssignDto> model)
        {
            Console.WriteLine($"📤 [RoleApiService] API'ye gidiyor => userId={userId}, rolesCount={model.Count}");
            foreach (var item in model)
            {
                Console.WriteLine($"    => (API Post) {item.Name}, Exists={item.Exists}");
            }

            var response = await _client.PostAsJsonAsync($"api/role/assign-roles/{userId}", model);
            response.EnsureSuccessStatusCode(); 
        }
    }
}
