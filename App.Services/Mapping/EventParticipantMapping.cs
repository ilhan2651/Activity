using App.Dto.EventParticipantDtos;
using App.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Mapping
{
    public class EventParticipantMapping : Profile
    {
        public EventParticipantMapping()
        {
            CreateMap<JoinEventDto, EventParticipant>();
            CreateMap<EventParticipant, JoinEventDto >();
            CreateMap<EventParticipant, ParticipantListDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserFullName))
                .ForMember(dest => dest.UserPictureUrl, opt => opt.MapFrom(src => src.User.UserProfilePictureUrl));
        }
    }
}
