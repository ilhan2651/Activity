using App.Dto.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Services.Abstract
{
    public interface IUserService
    {
        Task<List<UserListDto>> GetUserListAsync();
    }
}
