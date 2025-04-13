using App.Entities;
using App.Repositories.Context;
using App.Repositories.Generic;
using App.Repositories.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Repositories.Concrete
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        private readonly ActivityProjectContext _context;
        public EventRepository(ActivityProjectContext context):base(context)
        {
            _context   = context;
        }

        public async Task<List<Event>> GetAllWithCreatedBy()
        {
            var events= await _context.Events
                .Include(e=>e.CreatedBy)
                .OrderByDescending(e=>e.Date)
                .ToListAsync();
           

            return events;
        }
        public async Task<Event> GetEventWithCreatedBy(int id)
        {
            var evnt = await _context.Events
                .Where(e => e.EventId == id)
                .Select(e => new Event
                {
                    EventId = e.EventId,
                    EventTitle = e.EventTitle,
                    EventContent = e.EventContent,
                    EventLocation = e.EventLocation,
                    EventImageUrl = e.EventImageUrl,
                    Date = e.Date,
                    CreatedBy = new AppUser { UserFullName = e.CreatedBy.UserFullName },
                    Comments = e.Comments.ToList()
                })
                .FirstOrDefaultAsync();

            return evnt;
        }


        public async Task<Event?> GetWeeklyEventAsync()
        {
            var now = DateTime.UtcNow;

            // Önce gelecekteki en yakın etkinliği bul
            var futureEvent = await _context.Events
                .Include(e => e.CreatedBy)
                .Where(e => e.Date >= now)
                .OrderBy(e => e.Date)
                .FirstOrDefaultAsync();

            if (futureEvent != null)
                return futureEvent;

            // Eğer gelecekte etkinlik yoksa, geçmişten bugüne en yakın olanı getir
            return await _context.Events
                .Include(e => e.CreatedBy)
                .Where(e => e.Date < now)
                .OrderByDescending(e => e.Date)
                .FirstOrDefaultAsync();
        }
        public async Task<Event?> GetLastEvent()
        {
             return await _context.Events
                .Include(e => e.CreatedBy)
                .OrderByDescending(e => e.Date)
                .FirstOrDefaultAsync();
        }
    }
}
    