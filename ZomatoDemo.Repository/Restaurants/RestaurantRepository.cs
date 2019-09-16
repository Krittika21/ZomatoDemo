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
        public Location GetRestaurantLocation(long restaurantId)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Restaurant> GetRestaurants()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Restaurant> GetUserRestaurants(long userId, long restaurantId)
        {
            throw new NotImplementedException();
        }

        //post
        public object AddLocation(Location location)
        {
            throw new NotImplementedException();
        }
        public object AddAllRestaurants(Restaurant restaurants)
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
