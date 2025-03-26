using App.Entities;

namespace App.Dto.EventDtos
{
    public class EventListDto
    {
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public string EventContent { get; set; }
        public string EventLocation { get; set; }
        public string EventImageUrl { get; set; }

        public bool IsComeTrue { get; set; }
        public int MaxParticipants { get; set; }
        public DateTime Date { get; set; }
        public string CreatedBy { get; set; }
        public int CommentsCount { get; set; }
    }
}
