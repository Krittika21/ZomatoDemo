using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        public Task<T> GetById<T>(object id) where T : class
        {
            return CreateDbSet<T>().FindAsync(id);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public IQueryable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return CreateDbSet<T>().Where(predicate);
        }

        private DbSet<T> CreateDbSet<T>() where T : class
        {
            return _dbContext.Set<T>();
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return CreateDbSet<T>().AsQueryable();
        }
        public async Task<T> FirstAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await CreateDbSet<T>().Where(predicate).FirstAsync();
        }
        public async Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await CreateDbSet<T>().Where(predicate).FirstOrDefaultAsync();
        }
        public T FirstOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return CreateDbSet<T>().Where(predicate).FirstOrDefault();
        }
        public Task<T> SingleAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return CreateDbSet<T>().Where(predicate).SingleAsync();
        }
        public async Task AddRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            await CreateDbSet<T>().AddRangeAsync(entities);
        }
        public async Task<EntityEntry<T>> AddAsync<T>(T entity) where T : class
        {
            return await CreateDbSet<T>().AddAsync(entity);
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public EntityState Entry<T>(T entity) where T : class
        {
            return _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Remove<T>(T entity) where T : class
        {
            CreateDbSet<T>().Remove(entity);
        }
        public void RemoveRange<T>(IEnumerable<T> entities) where T : class
        {
            CreateDbSet<T>().RemoveRange(entities);
        }
        public async Task<T> FindAsyncById<T>(int id) where T : class
        {
            return await CreateDbSet<T>().FindAsync(id);
        }

        public void Update<T>(T entity) where T : class
        {
            CreateDbSet<T>().Update(entity);
        }
    }
}
