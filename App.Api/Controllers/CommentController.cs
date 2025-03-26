using App.Dto.CommentDto;
using App.Entities;
using App.Services.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpGet("listByEventId/{eventId}")]
        public async Task<IActionResult> GetCommentListByEventId(int eventId)
        {
            var commentList= await _commentService.GetCommentsByEventId(eventId);
            if (commentList == null || !commentList.Any())  
            {
                return NotFound("Bu etkinliğe hiç yorum yapılmamış. İlk yorumu yapmaya ne dersin.");
            }
            return Ok(commentList);
        }
        [HttpPost("AddComment")]
        public async Task<IActionResult> CreateComment([ FromBody] CreateCommentDto createDto)
        {
            if (string.IsNullOrEmpty(createDto.Content))
            {
                return BadRequest(new { success = false, message = "Yorum boş olamaz." });
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new { success = false, message = "Kimlik doğrulama başarısız." });
            }

            if (!int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new { success = false, message = "Geçersiz kullanıcı kimliği." });
            }

            var newComment = new Comment
            {
                EventId = createDto.EventId,
                Content = createDto.Content,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            await _commentService.AddComment(newComment);
            return Ok(new { success = true, message = "Yorum başarıyla eklendi!" });
        }
    
        [HttpDelete]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var deletedComment=await _commentService.DeleteEventAsync(commentId);
            if (!deletedComment)
            {
                NotFound(new { message = "Silmek istediğiniz yorum bulunamadı." });
            }
            return Ok(new {message="Yorum başarıyla silindi."});
        }
    }
}
