using App.Dto.UserDtos;
using Azure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace App.Services.Services.ApiServices.Concrete
{
    public class AuthApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthApiService(HttpClient httpClient, IHttpContextAccessor contextAccessor)
        {
            _httpClient = httpClient;
            _contextAccessor = contextAccessor;
        }

        public async Task<bool> RegisterAsync(RegisterDto registerDto)
        {
            var json=JsonSerializer.Serialize(registerDto);
            var content= new StringContent(json,Encoding.UTF8,"application/json");
            var response = await _httpClient.PostAsync("api/Auth/register",content);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> LoginAsync(LoginDto loginDto)
        {
            var json= JsonSerializer.Serialize(loginDto);
            var content= new StringContent(json, Encoding.UTF8,"application/json");
             _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            var response = await _httpClient.PostAsync("api/Auth/login", content);
            if (!response.IsSuccessStatusCode)
                return false;
            var token = await response.Content.ReadAsStringAsync();
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddHours(1)
            };
            _contextAccessor.HttpContext?.Response.Cookies.Append("AuthToken",token,cookieOptions);
            return true;

        }
        public async Task<bool> LogoutAsync()
        {
            var response = await _httpClient.PostAsync("api/Auth/logout", null);

            if (!response.IsSuccessStatusCode)
                return false;

            return true;
        }

    }
}
