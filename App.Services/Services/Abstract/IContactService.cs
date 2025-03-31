using App.Dto.ContactDto;
using App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Services.Abstract
{
    public interface IContactService
    {
        Task AddContact(CreateContactDto  dto);
        Task RemoveContact(int id);
        Task<List<ListContactDto>> GetAllContactsByDateAsync();

    }
}
