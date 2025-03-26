using App.Dto.CommentDto;
using App.Dto.EventDtos;
using App.Services.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace App.Services.Services.ApiServices.Concrete
{
    public class CommentApiService
    {
        private readonly HttpClient _httpClient;

        public CommentApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ListCommentDto>?> GetCommentsByEventId(int eventId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Comment/listByEventId/{eventId}");

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<ListCommentDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> PostComment(CreateCommentDto commentDto, string token)
        {
            try
            {
                var json = JsonSerializer.Serialize(commentDto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using var request = new HttpRequestMessage(HttpMethod.Post, "api/CommentApi/AddComment");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                request.Content = content;

                var response = await _httpClient.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Yorum gönderme hatası: {ex.Message}");
                return false;
            }
        }


    }
}
