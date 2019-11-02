using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Application_Classes;
using ZomatoDemo.Repository.Authentication;
using ZomatoDemo.Repository.Restaurants;
using ZomatoDemo.Repository.Users;
using ZomatoDemo.Web.Models;

namespace ZomatoDemo.Repository.UnitOfWork
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private readonly ZomatoDbContext _dbContext;
        private readonly UserManager<UserAC> _userManager;
        private IUserRepository _User;
        private readonly IJwtFactory _jwtFactory;

        private IRestaurantsRepository _Restaurant;
        public UnitOfWorkRepository(ZomatoDbContext dbContext, UserManager<UserAC> userManager, IJwtFactory jwtFactory)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
            System.Diagnostics.Debug.Write("jhsg");
            this._jwtFactory = jwtFactory;
        }
        
        public IUserRepository User
        {
            get
            {
                if (_User == null)
                {
                    _User = new UserRepository(_dbContext, _userManager, _jwtFactory);
                }
                return _User;
            }
        }
        public IRestaurantsRepository Restaurant
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

