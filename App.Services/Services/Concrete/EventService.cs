using App.Dto.EventDtos;
using App.Entities;
using App.Repositories.Repositories.Abstract;
using App.Services.Services.Abstract;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Services.Concrete
{
    public class EventService :IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        public EventService(IEventRepository eventRepository,IMapper mapper) 
        {
            _eventRepository    = eventRepository;
            _mapper = mapper;
        }
       
        public async Task<bool> CreateEventAsync(CreateEventDto eventDto,int userId)
        {
            try
            {
                var newEvent = new Event
                {
                    EventTitle = eventDto.EventTitle,
                    EventContent = eventDto.EventContent,
                    EventLocation = eventDto.EventLocation,
                    EventImageUrl = eventDto.EventImageUrl,
                    MaxParticipants = eventDto.MaxParticipants,
                    Date = DateTime.SpecifyKind(eventDto.Date, DateTimeKind.Utc),
                    CreatedById = userId
                };
                await _eventRepository.Add(newEvent);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }
        public async Task<EventListDto> GetEventWithCreatedByAsync(int id)
        {
            var evnt=await _eventRepository.GetEventWithCreatedBy(id);
            return _mapper.Map<EventListDto>(evnt);
        }
        public async Task<Event> UpdateEventAsync(int id, UpdateEventDto eventDto)
        {
            var existingEvent = await _eventRepository.GetById(id);
            if (existingEvent == null) return null;
            _mapper.Map(eventDto, existingEvent);

            await _eventRepository.Update(existingEvent);
            return existingEvent;

        }
        public async Task<bool> DeleteEventAsync(int id)
        {
            var existingEvent = await _eventRepository.GetById(id);
            if (existingEvent == null) return false;

            await _eventRepository.Delete(id);
            return true;
        }

        public async Task<List<EventListDto>> GetAllEventsWithCreated()
        {
            var listEvent = await _eventRepository.GetAllWithCreatedBy();
            return _mapper.Map<List<EventListDto>>(listEvent);
        }

        public async Task<EventDto> GetEventWeeklyAsync()
        {
            var eventWeekly= await _eventRepository.GetWeeklyEventAsync();
            if (eventWeekly==null)
            {
                var lastEvent= await  _eventRepository.GetLastEvent();
                return _mapper.Map<EventDto>(lastEvent);
            }
            return _mapper.Map<EventDto>(eventWeekly);
        }
    }
}
