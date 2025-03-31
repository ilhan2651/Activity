using App.Dto.ContactDto;
using App.Entities;
using App.Repositories.Repositories.Abstract;
using App.Services.Services.Abstract;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public ContactService(IContactRepository contactRepository,IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }
        public async Task AddContact(CreateContactDto dto)
        {
            var contact = _mapper.Map<Contact>(dto);
            await _contactRepository.Add(contact);
         
        }

        public async Task<List<ListContactDto>> GetAllContactsByDateAsync()
        {
            
          var contactList= await _contactRepository.GetAllContactsByDate();
            return _mapper.Map<List<ListContactDto>>(contactList);
        }

        public async Task RemoveContact(int id)
        {
            await _contactRepository.Delete(id);
        }
    }
}
