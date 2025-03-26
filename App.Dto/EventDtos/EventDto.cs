using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Dto.EventDtos
{
    public class EventDto
    {
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public string EventContent { get; set; }
        public string EventLocation { get; set; }
        public string EventImageUrl { get; set; }
        public DateTime Date { get; set; }
        public string CreatedBy { get; set; }


    }
}
