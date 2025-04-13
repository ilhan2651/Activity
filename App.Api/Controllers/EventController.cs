using App.Dto.EventDtos;
using App.Entities;
using App.Services.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }
        [HttpGet("allEvents")]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _eventService.GetAllEventsWithCreated();
           return Ok(events);
        }
        [HttpGet("byId/{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var evnt=await _eventService.GetEventWithCreatedByAsync(id);
            if (evnt == null) return NotFound(new { message = "Etkinlik bulunamadı." });
            return Ok(evnt);
        }
        [HttpPost("createEvent")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto eventDto)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "Kullanıcı kimliği bulunamadı." });
            }

            int userId = int.Parse(userIdClaim);
            var result = await _eventService.CreateEventAsync(eventDto, userId);

            if (!result)
                return BadRequest(new { message = "Etkinlik oluşturulamadı." });

            return Ok(new { message = "Etkinlik başarıyla oluşturuldu." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id,  [FromBody] UpdateEventDto eventDto)
        {
            if (eventDto==null)
            {
                return BadRequest(new { message = "Güncelleme Verisi Boş Olamaz." });
            }
            var updatedEvent=await _eventService.UpdateEventAsync(id,eventDto);
            if (updatedEvent == null)
            {
                return NotFound(new { message = $"ID {id} olan etkinlik bulunamadı." });
            }
            return Ok(updatedEvent);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
           var isDeleted= await _eventService.DeleteEventAsync(id);
            if (!isDeleted)
            {
                return NotFound(new { message = "Silmek istediğiniz etkinlik bulunamadı." });
            }
            return Ok(new { message = "Etkinlik Başarıyla Silindi" });
        }
        [HttpGet("Weekly")]
        public async Task<IActionResult> GetEventWeekly()
        {
            var events =await _eventService.GetEventWeeklyAsync();
            if (events==null)
            {
                return NotFound(new { message = "Henüz hiç bir aktivite eklenmemiş." });
            }
            return Ok(events);
        }

    }
}
