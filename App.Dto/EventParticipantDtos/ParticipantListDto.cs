using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Dto.EventParticipantDtos
{
    public class ParticipantListDto
    {
        public string UserName { get; set; }
        public string UserPictureUrl { get; set; }
        public DateTime JoinedAt { get; set; }
    }
}
