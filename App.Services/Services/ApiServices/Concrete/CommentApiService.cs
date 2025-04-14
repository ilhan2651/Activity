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
        public async Task<List<ListCommentDtoModerator>> GetAllComents(string token)
        {
            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, "api/Comment/ListAll");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                    return new List<ListCommentDtoModerator>();

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<ListCommentDtoModerator>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
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
                using var request = new HttpRequestMessage(HttpMethod.Post, "api/Comment/AddComment");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var form = new MultipartFormDataContent
        {
            { new StringContent(commentDto.EventId.ToString()), "EventId" },
            { new StringContent(commentDto.Content), "Content" }
        };

                if (commentDto.CommentImage != null)
                {
                    var stream = commentDto.CommentImage.OpenReadStream();
                    form.Add(new StreamContent(stream), "CommentImage", commentDto.CommentImage.FileName);
                }

                request.Content = form;

                var response = await _httpClient.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Yorum gönderme hatası: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> DeleteComment(int id, string token)
        {
            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Delete, $"api/Comment/Delete/{id}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Yorum silme hatası: {ex.Message}");
                return false;
            }
        }


    }
}
