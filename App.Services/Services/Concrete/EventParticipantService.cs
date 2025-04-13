using App.Dto.EventParticipantDtos;
using App.Entities;
using App.Repositories.Repositories.Abstract;
using App.Services.Services.Abstract;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Services.Concrete
{
    public class EventParticipantService : IEventParticipantService
    {
        private readonly IMapper _mapper;
        private readonly IEventParticipantRepository _eventParticipantRepository;
     
        public EventParticipantService(IEventParticipantRepository eventParticipantRepository,IMapper mapper)
        {
            _eventParticipantRepository = eventParticipantRepository;
            _mapper = mapper;
        }
        public async Task JoinEventAsync(JoinEventDto dto)
        {
            var alreadyJoined = await _eventParticipantRepository.AlreadyJoined(dto.EventId, dto.UserId);
            if (alreadyJoined!=null)
            {
                throw new InvalidOperationException("Bu etkinliğe zaten katıldınız.");
            }
            var eventParticipant = _mapper.Map<EventParticipant>(dto);
            eventParticipant.JoinedAt = DateTime.UtcNow;
            await _eventParticipantRepository.Add(eventParticipant);
        }
        public async Task<bool> AlreadyJoined(int eventId, int userId)
        {
            var alreadyJoined = await _eventParticipantRepository.AlreadyJoined(eventId, userId);
            if (alreadyJoined==null)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var deletedValue=await _eventParticipantRepository.GetById(id);
            if (deletedValue == null)
            {
                return false;
            }
            await _eventParticipantRepository.Delete(id);
            return true;
        }
        public async Task<List<ParticipantListDto>> GetParticipantListAsync(int eventId)
        {
            var participantList= await _eventParticipantRepository.GetParticipantsByEventId(eventId);
            if (participantList == null)
            {
                return new List<ParticipantListDto>();
            }
            
            return _mapper.Map<List<ParticipantListDto>>(participantList);
        }
        
      
    }
}
