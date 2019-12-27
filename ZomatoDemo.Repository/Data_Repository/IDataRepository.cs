using Microsoft.EntityFrameworkCore;
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
        //IEnumerable<T> GetAll();
        //T GetById(object id);
        //void Insert(T obj);
        //void Update(T obj);
        //void Delete(object id);
        void Save();
        IQueryable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : class;
        IQueryable<T> GetAll<T>() where T : class;
       // DbSet<T> CreateDbSet<T>() where T : class;
        Task<T> FirstAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
        T FirstOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<T> SingleAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
    }
}
