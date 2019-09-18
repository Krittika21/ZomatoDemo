using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Application_Classes;
using ZomatoDemo.DomainModel.Models;

namespace ZomatoDemo.Repository.Restaurants
{
    public class RestaurantRepository : IRestaurantRepository
    {

        private DbContext _dbContext;

        public RestaurantRepository(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        //get
        public Location GetRestaurantLocation(int restaurantId)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Restaurant> GetRestaurants()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Restaurant> GetUserRestaurants(int userId, int restaurantId)
        {
            throw new NotImplementedException();
        }

        //post
        public async Task<IEnumerable<LocationAC>> AddLocation(List<LocationAC> locationAC)
        {
            //Country map;
            //List<Location> locations = new List<Location>();
            //foreach (var item in locationAC)
            //{
            //    map = await _dbContext.Country.FirstAsync(x => x.ID == item.ID);
            //    locationAC.Add(new Location
            //    {
            //        City = new City
            //        {
            //            CityName = item.
            //        },
            //    });
            //}
            //_dbContext.locations.AddRange(locationAC);
            //await _dbContext.SaveChangesAsync();

            throw new NotImplementedException();
        }
        public IEnumerable<Restaurant> AddAllRestaurants(List<Restaurant> restaurants)
        {
            throw new NotImplementedException();
        }

        //put
        public Restaurant EditRestaurant([FromBody] List<Restaurant> restaurants)
        {
            throw new NotImplementedException();
        }

        //delete
        public bool DeleteRestaurant(int restaurantId)
        {
            throw new NotImplementedException();
        }
    }
}
