using App.Dto.UserDtos;
using App.Entities;
using App.Services.Services.Abstract;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace App.Services.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<AppUser> userManager,IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;   
        }

        public async Task<List<UserListDto>> GetUserListAsync()
        {
            var dto=await _userManager.Users.ToListAsync();
            if (dto == null)
            {
                return new List<UserListDto> { new UserListDto() };
            }
            return _mapper.Map<List<UserListDto>>(dto);
        }
    }
}
