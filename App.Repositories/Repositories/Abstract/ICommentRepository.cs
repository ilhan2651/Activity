using App.Entities;
using App.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Repositories.Abstract
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
          Task<List<Comment>> GetCommentsWithWritersByEventId(int eventId);
        Task<List<Comment>> GetAllComments();

    }
}
