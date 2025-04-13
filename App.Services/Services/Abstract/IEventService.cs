using App.Dto.EventDtos;
using App.Entities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Services.Abstract
{
    public interface IEventService 
    {

        Task<bool> CreateEventAsync(CreateEventDto eventDto,int UserId);
        Task<Event> UpdateEventAsync(int id, UpdateEventDto eventDto);
         Task<bool> DeleteEventAsync(int id);
        Task<List<EventListDto>> GetAllEventsWithCreated();
        Task<EventListDto> GetEventWithCreatedByAsync(int id);
        Task<EventDto> GetEventWeeklyAsync();


    }
}
