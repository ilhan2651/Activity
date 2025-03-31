using App.Dto.ContactDto;
using App.Entities;
using App.Services.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMailService _mailService;
        public ContactController(IContactService contactService, IMailService mailService)
        {
            _contactService = contactService;
            _mailService = mailService;
        }

        [HttpGet("get-all-contacts")]
        public async Task<IActionResult> GetAllContacts()
        {
            var contacts = await _contactService.GetAllContactsByDateAsync();
            return Ok(contacts);
        }
        [HttpPost("add-contact")]
        public async Task<IActionResult> AddContact([FromBody] CreateContactDto dto)
        {
            await _contactService.AddContact(dto);
            await _mailService.SendContactMailAsync(dto.Name, dto.Email, dto.Subject, dto.Message);

            return Created("", new { message="İletişim mesajı başarıyla gönderildi."});
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            await _contactService.RemoveContact(id);
            return Ok();
        }
    }
}
