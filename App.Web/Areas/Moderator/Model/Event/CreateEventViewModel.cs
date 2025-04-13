using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace App.Web.Areas.Moderator.Model.Event
{
    public class CreateEventViewModel
    {
        [Required(ErrorMessage = "Etkinlik başlığı zorunludur.")]
        [MaxLength(100, ErrorMessage = "En fazla 100 karakter olabilir.")]
        public string EventTitle { get; set; } = default!;

        [Required(ErrorMessage = "Etkinlik açıklaması zorunludur.")]
        public string EventContent { get; set; } = default!;

        [Required(ErrorMessage = "Konum zorunludur.")]
        public string EventLocation { get; set; } = default!;

        [Range(1, int.MaxValue, ErrorMessage = "Katılımcı sayısı en az 1 olmalıdır.")]
        public int MaxParticipants { get; set; }

        [Required(ErrorMessage = "Tarih zorunludur.")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }




        [Required(ErrorMessage = "Bir görsel yüklemelisiniz.")]
        public IFormFile EventImage { get; set; } = default!;
    }
}
