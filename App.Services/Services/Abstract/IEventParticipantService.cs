using App.Dto.EventParticipantDtos;
using App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Services.Abstract
{
    public interface IEventParticipantService 
    {
        Task JoinEventAsync(JoinEventDto dto);
        Task<List<ParticipantListDto>> GetParticipantListAsync(int eventId);
        Task<bool> DeleteAsync(int id);
        Task<bool> AlreadyJoined(int eventId, int userId);


    }
}
