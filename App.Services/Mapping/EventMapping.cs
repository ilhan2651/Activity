using App.Dto.EventDtos;
using App.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Mapping
{
    public class EventMapping : Profile
    {
        public EventMapping()
        {
            CreateMap<CreateEventDto, Event>();

            CreateMap<Event, EventListDto>()
    .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(
        src => src.CreatedBy != null && !string.IsNullOrEmpty(src.CreatedBy.UserFullName)
            ? src.CreatedBy.UserFullName
            : "Bilinmiyor"))
    .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(
        src => src.Comments.Count()));



            CreateMap<UpdateEventDto, Event>()
           .ForAllMembers(opt =>
               opt.Condition((src, dest, srcMember) => srcMember != null &&
                                                       !(srcMember is string str && string.IsNullOrEmpty(str)) &&
                                                       !(srcMember is DateTime dt && dt == default) &&
                                                       !(srcMember is int val && val == default)));


            CreateMap<Event, EventDto>()
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.UserFullName));
        }
    }
}
