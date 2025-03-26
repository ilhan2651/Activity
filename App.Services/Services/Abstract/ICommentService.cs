using App.Dto.CommentDto;
using App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Services.Abstract
{
    public interface ICommentService 
    {
        Task AddComment(Comment comment);
        Task<bool> DeleteEventAsync(int id);
        Task<List<ListCommentDto>> GetCommentsByEventId(int eventId);
    }
}
