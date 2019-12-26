using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Application_Classes;
using ZomatoDemo.Repository.Authentication;
using ZomatoDemo.Web.Models;

namespace ZomatoDemo.Repository.Data_Repository
{
    public class DataRepository<T> : IDataRepository<T> where T : class
    {
        private ZomatoDbContext _dbContext = null;
        private DbSet<T> table = null;

        public DataRepository(ZomatoDbContext _context)
        {
            _dbContext = _context;
            table = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
