using App.Entities;
using App.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace App.Repositories.Repositories.Abstract
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        Task<List<Event>> GetAllWithCreatedBy();
        Task<Event> GetEventWithCreatedBy(int id);
         Task<Event?> GetWeeklyEventAsync();
        Task<Event?> GetLastEvent();

    }
}
