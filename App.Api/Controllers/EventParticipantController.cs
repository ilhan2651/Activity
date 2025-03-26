using App.Dto.EventParticipantDtos;
using App.Services.Services.Abstract;
using App.Services.Services.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventParticipantController : ControllerBase
    {
        private readonly IEventParticipantService _eventParticipantService;
        public EventParticipantController(IEventParticipantService eventParticipantService)
        {
            _eventParticipantService = eventParticipantService;
        }

        [HttpPost("join")]
        public async Task<IActionResult> JoinEvent([FromBody] JoinEventDto dto)
        {
            try
            {
                await _eventParticipantService.JoinEventAsync(dto);
                return Ok(new { message = "Bu etkinliğe katılacağınızı belirttiniz." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipant(int id)
        {    
              var deletedValue = await _eventParticipantService.DeleteAsync(id);
            if (deletedValue)
            {
                return Ok(new { message = "Belirttiğiniz katılım kaldırıldı." });

            }
            return BadRequest(new {message="Katılımcı kaldırılamadı."});            
          
        }
        [HttpGet("event/{eventId}/participants")]
        public async Task<IActionResult> GetEventParticipants(int eventId)
        {
            var participants =await _eventParticipantService.GetParticipantListAsync(eventId);
            if (!participants.Any())
            {
                return NotFound(new { message = "Bu etkinliğe henüz kimse katılmamış." });
            }
            return Ok(participants);
        }

    }
}
 