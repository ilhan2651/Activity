using App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Generic
{
    public interface IGenericRepository<T> where T : class, IEntity
    {
        Task<List<T>> ListAll();
        Task Add(T entity);
        Task Delete(int id);
        Task Update(T entity);
        Task<T> GetById(int id);
        Task<T> GetByFilter(Expression<Func<T, bool>> filter);
        Task<List<T>> GetFilteredList(Expression<Func<T, bool>> filter);

    }
}
