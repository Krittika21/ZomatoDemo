using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ZomatoDemo.Repository.Data_Repository
{
    public interface IDataRepository
    {
        void Save();
        IQueryable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : class;
        IQueryable<T> GetAll<T>() where T : class;
        Task<T> FirstAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
        T FirstOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<T> SingleAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task AddRangeAsync<T>(IEnumerable<T> entity) where T : class;
        Task<EntityEntry<T>> AddAsync<T>(T entity) where T : class;
        Task SaveChangesAsync();
        EntityState Entry<T>(T entity) where T : class;
        void Remove<T>(T entity) where T : class;
        void RemoveRange<T>(IEnumerable<T> entity) where T : class;
        Task<T> FindAsyncById<T>(int id) where T : class;
        void Update<T>(T entity) where T : class;
    }
}
