using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Dto.CommentDto
{

    public class CreateCommentDtoMvc
    {
        public int EventId { get; set; }
        public string Content { get; set; } = null!;
        public IFormFile? CommentImage { get; set; } 
    }
}


