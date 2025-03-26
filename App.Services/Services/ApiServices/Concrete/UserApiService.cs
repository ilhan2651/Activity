using App.Dto.EventDtos;
using App.Dto.UserDtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace App.Services.Services.ApiServices.Concrete
{
    public class UserApiService
    {
        private readonly HttpClient _httpClient;

        public UserApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<UserListDto?>> GetUsersAsync()
        {
            try
            {
                var response =await _httpClient.GetAsync("api/User/getUsers");
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<UserListDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch 
            {

                return null;
            }
        }
    }
}
