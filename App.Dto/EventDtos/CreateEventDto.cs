using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Dto.EventDtos
{
    public class CreateEventDto
    {
        public string EventTitle { get; set; } = default!;
        public string EventContent { get; set; } = default!;
        public string EventLocation { get; set; } = default!;
        public string EventImageUrl { get; set; }

        public int MaxParticipants { get; set; }
        public DateTime Date { get; set; }

    }
}
