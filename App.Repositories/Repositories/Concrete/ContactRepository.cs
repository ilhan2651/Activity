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
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        private readonly ActivityProjectContext _context;
        public ContactRepository(ActivityProjectContext context):base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contact>> GetAllContactsByDate()
        {
            return await _context.Contacts
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }
    }
}
