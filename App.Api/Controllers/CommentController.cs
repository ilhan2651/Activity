﻿using App.Dto.CommentDto;
using App.Entities;
using App.Services.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Security.Claims;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<AppUser> _userManager;

        public CommentController(ICommentService commentService, IWebHostEnvironment env, UserManager<AppUser> userManager)
        {
            _commentService = commentService;
            _env = env;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost("AddComment")]
        public async Task<IActionResult> AddComment([FromForm] CreateCommentDto dto)
        {
            Console.WriteLine(">> User Identity: " + User.Identity?.Name);
            Console.WriteLine(">> Authenticated: " + User.Identity?.IsAuthenticated);
            Console.WriteLine(">> Claims: " + string.Join(", ", User.Claims.Select(c => $"{c.Type}: {c.Value}")));

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized();

            var user = await _userManager.FindByIdAsync(userIdClaim);
            if (user == null) return NotFound("Kullanıcı bulunamadı.");

            string? imagePath = null;

            if (dto.CommentImage != null && dto.CommentImage.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(dto.CommentImage.FileName);

                // 🔥 Doğrudan App.Web'in yolunu veriyoruz (bir üst klasöre çık ve App.Web'e gir)
                var folder = Path.GetFullPath(Path.Combine("..", "App.Web", "wwwroot", "uploads", "comments"));

                Directory.CreateDirectory(folder); // Klasör yoksa oluştur

                var filePath = Path.Combine(folder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await dto.CommentImage.CopyToAsync(stream);

                // MVC'nin erişeceği şekilde URL oluşturuyoruz
                imagePath = "/uploads/comments/" + fileName;
            }

            var comment = new Comment
            {
                EventId = dto.EventId,
                Content = dto.Content,
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                EventImage = imagePath
            };

            await _commentService.AddComment(comment);
            return Ok(new { message = "Yorum başarıyla eklendi." });
        }

        [HttpGet("ListByEventId/{eventId}")]
        public async Task<IActionResult> GetByEvent(int eventId)
        {
            var comments = await _commentService.GetCommentsByEventId(eventId);
            return Ok(comments);
        }
    }
}
