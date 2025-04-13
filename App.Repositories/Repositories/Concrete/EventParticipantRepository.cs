using App.Entities;
using App.Repositories.Context;
using App.Repositories.Generic;
using App.Repositories.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Repositories.Concrete
{
    public class EventParticipantRepository : GenericRepository<EventParticipant>,IEventParticipantRepository
    {
        private readonly ActivityProjectContext _context;
        public EventParticipantRepository(ActivityProjectContext context):base(context)
        {
            _context=context;
        }

        public async Task<EventParticipant> AlreadyJoined(int eventId, int userId)
        {
          return await _context.EventParticipants
        .FirstOrDefaultAsync(p => p.EventId == eventId && p.UserId == userId);
        }

        public async Task<List<EventParticipant>> GetParticipantsByEventId(int eventId)
        {
            return await _context.EventParticipants
               .Where(p => p.EventId == eventId)
               .Include(p => p.User)
               .ToListAsync();
        }
        
               
    }
}
