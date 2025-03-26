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
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet("get-all-contacts")]
        public async Task<IActionResult> GetAllContacts()
        {
            var contacts = await _contactService.GetAllContactsByDateAsync();
            return Ok(contacts);
        }
        [HttpPost("add-contact")]
        public async Task<IActionResult> AddContact([FromBody] Contact contact)
        {
            await _contactService.AddContact(contact);
            return Ok();
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            await _contactService.RemoveContact(id);
            return Ok();
        }
    }
}
