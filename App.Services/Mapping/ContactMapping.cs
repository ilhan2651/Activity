using App.Dto.ContactDto;
using App.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Mapping
{
    public class ContactMapping : Profile
    {
        public ContactMapping()
        {
            CreateMap<Contact, ListContactDto>();
            CreateMap<CreateContactDto, Contact>();

            CreateMap<Contact, CreateContactDto>();
        }
    }
}
