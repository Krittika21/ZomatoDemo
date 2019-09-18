using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZomatoDemo.Repository.Restaurants;
using ZomatoDemo.Repository.Users;

namespace ZomatoDemo.Repository.UnitOfWork
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private readonly DbContext _dbContext;
        public UnitOfWorkRepository(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        private IUserRepository _User;

        private IRestaurantRepository _Restaurant;
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
        public IRestaurantRepository Product
        {
            get
            {
                if (_Restaurant == null)
                {
                    _Restaurant = new RestaurantRepository(_dbContext);
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
}
