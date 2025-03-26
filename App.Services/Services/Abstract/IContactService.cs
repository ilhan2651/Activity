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
        Task AddContact(Contact contact);
        Task RemoveContact(int id);
        Task<IEnumerable<Contact>> GetAllContactsByDateAsync();

    }
}
