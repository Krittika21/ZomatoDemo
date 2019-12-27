using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Application_Classes;
using ZomatoDemo.Repository.Authentication;
using ZomatoDemo.Web.Models;

namespace ZomatoDemo.Repository.Data_Repository
{
    public class DataRepository : IDataRepository
    {
        private ZomatoDbContext _dbContext;
        public DataRepository(ZomatoDbContext _context)
        {
            _dbContext = _context;
        }

        //public IEnumerable<T> GetAll()
        //{
        //    return table.ToList();
        //}

        public Task<T> GetById<T>(object id) where T : class
        {
            return CreateDbSet<T>().FindAsync(id);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return CreateDbSet<T>().Where(predicate);
        }

        private DbSet<T> CreateDbSet<T>() where T : class
        {
            return _dbContext.Set<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<T> GetAll<T>() where T : class
        {
            return CreateDbSet<T>().AsQueryable();
        }
        public Task<T> FirstAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return CreateDbSet<T>().Where(predicate).FirstAsync();
        }
        public Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return CreateDbSet<T>().Where(predicate).FirstOrDefaultAsync();
        }
        public T FirstOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return CreateDbSet<T>().Where(predicate).FirstOrDefault();
        }
        public Task<T> SingleAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return CreateDbSet<T>().Where(predicate).SingleAsync();
        }
        public IEnumerable<T> AddRangeAsync<T>(IEnumerable<T>) where T : class
        {
            
        }
        public Task<T> AddAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return CreateDbSet<T>().Where(predicate).AddAsync();
        }
        
    }
}
