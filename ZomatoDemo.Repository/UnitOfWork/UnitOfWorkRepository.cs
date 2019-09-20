using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZomatoDemo.Repository.Restaurants;
using ZomatoDemo.Repository.Users;
using ZomatoDemo.Web.Models;

namespace ZomatoDemo.Repository.UnitOfWork
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private readonly ZomatoDbContext _dbContext;
        public UnitOfWorkRepository(ZomatoDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        private IUserRepository _User;

        private IRestaurantsRepository _Restaurant;
        public IUserRepository User
        {
            get
            {
                if (_User == null)
                {
                    _User = new UserRepository(_dbContext);
                }
                return _User;
            }
        }
        public IRestaurantsRepository Product
        {
            get
            {
                if (_Restaurant == null)
                {
                    _Restaurant = new RestaurantsRepository(_dbContext);
                }
                return _Restaurant;
            }
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public int Complete()
        {
            return _dbContext.SaveChanges();
        }
        public void Dispose() => _dbContext.Dispose();

    }
}

