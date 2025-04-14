using App.Dto.CommentDto;
using App.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Mapping
{
    public class CommentMapping : Profile
    {
        public CommentMapping()
        {
            CreateMap<Comment, ListCommentDto>()
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserFullName))
               .ForMember(dest => dest.UserImage, opt => opt.MapFrom(src => src.User.UserProfilePictureUrl));
            CreateMap<CreateCommentDto, Comment>();

            CreateMap<Comment, ListCommentDtoModerator>()
           .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserFullName))
           .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.Event.EventTitle));

        }
    }
}
