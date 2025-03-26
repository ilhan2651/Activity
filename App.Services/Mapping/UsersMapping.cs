using App.Dto.UserDtos;
using App.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Mapping
{
    public class UsersMapping :Profile 
    {
        public UsersMapping()
        {
            CreateMap<AppUser,UserListDto>();
        }
    }
}
