﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Dto.CommentDto
{
    public class ListCommentDtoModerator
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; }
        public string EventName { get; set; }
        public string? EventImage { get; set; }

    }
}
