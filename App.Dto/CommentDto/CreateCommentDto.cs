using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace App.Dto.CommentDto
{
    public class CreateCommentDto
    {
        public int EventId { get; set; }
        public string Content { get; set; } = null!;

        // Bu sadece yol, dosyanın veritabanına kaydedilecek yolu
        public string? EventImage { get; set; }

        // Bu, sadece dosya seçildiğinde formdan gelen dosya olacak
        [JsonIgnore]
        public IFormFile? CommentImage { get; set; }
    }
}
