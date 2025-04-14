using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace App.Dto.CommentDto
{
    public class CreateCommentDto
    {
        public int EventId { get; set; }
        public string Content { get; set; } = null!;

        public string? EventImage { get; set; }

        [JsonIgnore]
        public IFormFile? CommentImage { get; set; }
    }
}
