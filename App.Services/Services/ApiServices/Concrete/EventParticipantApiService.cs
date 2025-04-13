using App.Dto.EventDtos;
using App.Dto.EventParticipantDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace App.Services.Services.ApiServices.Concrete
{
    public class EventParticipantApiService
    {
        private readonly HttpClient _httpClient;

        public EventParticipantApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<ParticipantListDto>?> GetParticipantsOfWeeklyEvents(int eventId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/EventParticipant/event/{eventId}/participants");

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<ParticipantListDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                return null;
            }
        }
        public async Task<bool> AddParticipant(JoinEventDto joinDto)
        {
            try
            {
                var json = JsonSerializer.Serialize(joinDto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/EventParticipant/join", content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                return false;
            }

        }
        public async Task<AlreadyJoinedResponseDto?> GetJoinStatus(int eventId, int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/EventParticipant/has-joined?eventId={eventId}&userId={userId}");

                if (!response.IsSuccessStatusCode)
                    return null;

                var json = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<AlreadyJoinedResponseDto>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                return null;
            }
        }
        public async Task<bool> DeleteParticipant(int participantId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/EventParticipant/delete/{participantId}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Silme hatası: {ex.Message}");
                return false;
            }
        }


    }

}

