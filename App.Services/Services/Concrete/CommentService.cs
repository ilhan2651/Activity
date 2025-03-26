using App.Dto.CommentDto;
using App.Entities;
using App.Repositories.Repositories.Abstract;
using App.Repositories.Repositories.Concrete;
using App.Services.Services.Abstract;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Services.Concrete
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        IMapper _mapper;
        public CommentService(ICommentRepository commentRepository,IMapper mapper)
        {
            _commentRepository  = commentRepository;
            _mapper = mapper;
        }

        public async Task<List<ListCommentDto>> GetCommentsByEventId(int eventId)
        {
            var commentList= await _commentRepository.GetCommentsWithWritersByEventId(eventId);
            return _mapper.Map<List<ListCommentDto>>(commentList);
        }
        public async Task AddComment(Comment comment)
        {
            comment.CreatedAt= DateTime.UtcNow;
            await _commentRepository.Add(comment);
        }
        public async Task<bool> DeleteEventAsync(int id)
        {
            var existingEvent = await _commentRepository.GetById(id);
            if (existingEvent == null) return false;

            await _commentRepository.Delete(id);
            return true;
        }
    }
}
