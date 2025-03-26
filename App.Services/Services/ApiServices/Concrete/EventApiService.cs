using App.Dto.EventDtos;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace App.Services.Services.ApiServices.Concrete
{
    public class EventApiService
    {
        private readonly HttpClient _httpClient;

        public EventApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EventDto?> GetWeeklyActivitiesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Event/Weekly");

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<EventDto>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<EventListDto?>> GetEventsAllWeek()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Event/allEvents");

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<EventListDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch
            {
                return null;
            }
        }


        public async Task<EventListDto?> GetEventById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Event/byId/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<EventListDto>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch
            {
                return null;
            }
        }

    }
}