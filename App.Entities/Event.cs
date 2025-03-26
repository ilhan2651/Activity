using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities
{
    public class Event: IEntity
    {
        public int EventId { get; set; }
        public string EventTitle { get; set; } = default!;
        public string EventContent { get; set; } = default!;
        public string EventLocation { get; set; } = default!;
        public string EventImageUrl { get; set; }
        public bool IsComeTrue { get; set; }
        public int MaxParticipants { get; set; }
        public DateTime Date { get; set; } 
        public int CreatedById { get; set; }
        public AppUser CreatedBy { get; set; }
        public ICollection<EventParticipant> Participants { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
