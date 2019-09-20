using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Application_Classes;
using ZomatoDemo.DomainModel.Models;
using ZomatoDemo.Web.Models;

namespace ZomatoDemo.Repository.Restaurants
{
    public class RestaurantsRepository : IRestaurantsRepository
    {

        private ZomatoDbContext _dbContext;

        public RestaurantsRepository(ZomatoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //get
        public async Task<Location> GetRestaurantLocation(int restaurantId)
        {
            await _dbContext.SaveChangesAsync();
            return ();
            //throw new NotImplementedException();
        }
        public async Task<IEnumerable<Restaurant>> GetRestaurants()
        {
            await _dbContext.SaveChangesAsync();
            return ();
            //throw new NotImplementedException();
        }
        public async Task<IEnumerable<Restaurant>> GetUserRestaurants(int userId, int restaurantId)
        {
            await _dbContext.SaveChangesAsync();
            return ();
            //throw new NotImplementedException();
        }

        //post
        public async Task<IEnumerable<LocationAC>> AddLocation([FromBody] List<LocationAC> locationAC)
        {
            Country map;
            List<Location> locations = new List<Location>();
            foreach (var item in locationAC)
            {
                map = await _dbContext.Country.FirstAsync(x => x.ID == item.ID);
                locations.Add(new Location
                {
                    City = new City
                    {
                        CityName = item.CityName
                    },
                    Country = map
                });
            }
            _dbContext.Location.AddRange(locations);
            await _dbContext.SaveChangesAsync();
            return (locationAC);

            //throw new NotImplementedException();
        }
        public async Task<IEnumerable<RestaurantAC>> AddAllRestaurants(List<RestaurantAC> restaurantAC)
        {
            List<Location> locate = new List<Location>();
            List<Restaurant> restaurants = new List<Restaurant>();

            foreach (var item in restaurantAC)
            {
                foreach (var elements in item.LocationID)
                {
                    locate.Add(await _dbContext.Location.FirstAsync(x => x.ID == elements));
                }
                List<Dishes> dish = new List<Dishes>();
                foreach (var elements in item.DishesName)
                {
                    dish.Add(new Dishes
                    {
                        DishesName = elements
                    });
                }
                restaurants.Add(new Restaurant
                {
                    RestaurantName = item.RestaurantName,
                    Location = locate,
                    Dishes = dish
                });
            }
            _dbContext.Restaurant.AddRange(restaurants);
            await _dbContext.SaveChangesAsync();
            return (restaurantAC);

            //throw new NotImplementedException();
        }
        public async Task<IEnumerable<OrderDetailsAC>> AddOrderDetails(List<OrderDetailsAC> detailsAC)
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            List<OrderDetails> orders = new List<OrderDetails>();

            foreach (var item in detailsAC)
            {
                foreach (var elements in item.RestaurantID)
                {
                    restaurants.Add(await _dbContext.Restaurant.FirstAsync(x => x.ID == elements));
                }
                List<User> user = new List<User>();
                foreach (var elements in item.UserName)
                {
                    user.Add(new User
                    {
                        Name = elements
                    });
                }
                orders.Add(new OrderDetails
                {
                    User = user,
                    Restaurant = restaurants
                });
            }
            //include sum of dishes' cost
            _dbContext.Restaurant.AddRange(restaurants);
            await _dbContext.SaveChangesAsync();
            return (detailsAC);
        }

        //put
        public async Task<Restaurant> EditRestaurant([FromBody] List<Restaurant> restaurants)
        {
            await _dbContext.SaveChangesAsync();
            throw new NotImplementedException();
        }

        //delete
        public async Task<bool> DeleteRestaurant(int restaurantId)
        {
            await _dbContext.SaveChangesAsync();
            throw new NotImplementedException();
        }
    }
}
