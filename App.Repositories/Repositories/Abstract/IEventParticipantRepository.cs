using App.Entities;
using App.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Repositories.Abstract
{
    public interface IEventParticipantRepository : IGenericRepository<EventParticipant>
    {
        Task<List<EventParticipant>> GetParticipantsByEventId(int eventId);
        Task<bool> AlreadyJoined(int eventId,int userId);

    }
}
