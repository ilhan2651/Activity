using App.Dto.UserDtos;
using App.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Services.Abstract
{
    public interface IAuthService
    {
        Task<string> LoginUser(LoginDto dto);
        Task<string> GenerateJwtToken(AppUser user);
        Task<IdentityResult> Register(RegisterDto registerDto);

        Task<AppUser> GetUserById(string userId);

    }
}
