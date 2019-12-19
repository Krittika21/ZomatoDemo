using AutoMapper;
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
        private readonly UserManager<User> _userManager;
        private IUserRepository _User;
        private readonly IJwtFactory _jwtFactory;
        private readonly IMapper _mapper;

        private IRestaurantsRepository _Restaurant;
        public UnitOfWorkRepository(ZomatoDbContext dbContext, UserManager<User> userManager, IJwtFactory jwtFactory, IMapper mapper)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            System.Diagnostics.Debug.Write("jhsg");
            _jwtFactory = jwtFactory;
            _mapper = mapper;
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
                    _Restaurant = new RestaurantsRepository(_dbContext, _mapper);
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

