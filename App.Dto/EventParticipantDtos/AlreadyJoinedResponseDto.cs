using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Dto.EventParticipantDtos
{
    public class AlreadyJoinedResponseDto
    {
        
            public bool Joined { get; set; }
            public int? ParticipantId { get; set; }
        

    }
}
