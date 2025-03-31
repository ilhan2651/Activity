using App.Dto.ContactDto;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace App.Services.Services.ApiServices.Concrete
{
    public class ContactApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContactApiService(HttpClient httpClient,IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor= httpContextAccessor;
            _httpClient = httpClient;
        }
        public async Task<List<ListContactDto>?> GetContactsOrderingByDate()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/contact/get-all-contacts");

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<ListContactDto>>( json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch 
            {
                return null;
            }

            
        }
        public async Task<string?> PostContact(CreateContactDto dto)
        {
            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(dto),  Encoding.UTF8 ,  "application/json" );

                var response = await _httpClient.PostAsync("api/contact/add-contact", jsonContent);

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var obj = JsonSerializer.Deserialize<JsonObject>(json);
                    return obj?["message"]?.ToString(); // "İletişim başarıyla eklendi."
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> DeleteContactAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/contact/delete/{id}");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

    }
}
