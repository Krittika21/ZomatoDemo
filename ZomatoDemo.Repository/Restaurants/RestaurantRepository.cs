using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ZomatoDemo.DomainModel.Models;

namespace ZomatoDemo.Repository.Restaurants
{
    public class RestaurantRepository : IRestaurantRepository
    {

        private DbContext dbContext;

        public RestaurantRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //get
        public IEnumerable<Restaurant> GetRestaurants()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Restaurant> GetUserRestaurants(long userId)
        {
            throw new NotImplementedException();
        }

        //post
        public object AddLocation(Location location)
        {
            throw new NotImplementedException();
        }
        public object AddRestaurant(Restaurant restaurants)
        {
            throw new NotImplementedException();
        }
        public object AddRestaurantToUser(long userId, long restaurantId)
        {
            throw new NotImplementedException();
        }

        //put
        public Restaurant EditRestaurant(long restaurantId)
        {
            throw new NotImplementedException();
        }

        //delete
        public bool DeleteRestaurant(long restaurantId)
        {
            throw new NotImplementedException();
        }
    }
}
