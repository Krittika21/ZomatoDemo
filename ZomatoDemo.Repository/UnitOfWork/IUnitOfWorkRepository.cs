﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZomatoDemo.Repository.Restaurants;
using ZomatoDemo.Repository.Users;

namespace ZomatoDemo.Repository.UnitOfWork
{
    public interface IUnitOfWorkRepository
    {
        
            IUserRepository User { get; }
            IRestaurantsRepository Restaurant { get; }
            //Task<int> CompleteAsync();
            //int Complete();
        
    }
}
