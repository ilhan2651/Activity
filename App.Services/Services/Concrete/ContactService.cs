using App.Entities;
using App.Repositories.Repositories.Abstract;
using App.Services.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Services.Concrete
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task AddContact(Contact contact)
        {
           await _contactRepository.Add(contact);
        }

        public async Task<IEnumerable<Contact>> GetAllContactsByDateAsync()
        {
            return await _contactRepository.GetAllContactsByDate();
        }

        public async Task RemoveContact(int id)
        {
            await _contactRepository.Delete(id);
        }
    }
}
