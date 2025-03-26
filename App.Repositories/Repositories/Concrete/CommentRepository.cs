using App.Entities;
using App.Repositories.Context;
using App.Repositories.Generic;
using App.Repositories.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Repositories.Concrete
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        private readonly ActivityProjectContext _context;
        public CommentRepository(ActivityProjectContext context):base(context)
        {
            _context = context;
        }
        public async Task<List<Comment>> GetCommentsWithWritersByEventId(int eventId)
        {
           return await _context.Comments
                .Include(c => c.User)
                .Where(c => c.EventId == eventId)
                .ToListAsync();
        }
    }
}
