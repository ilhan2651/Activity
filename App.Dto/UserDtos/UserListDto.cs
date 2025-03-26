using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Dto.UserDtos
{
    public class UserListDto
    {
        public string UserFullName { get; set; } = default!;
        public string UserProfilePictureUrl { get; set; } = default!;
    }
}
