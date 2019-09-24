using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        //GET
        //get locations as per restaurant Id
        public async Task<ICollection<Location>> GetRestaurantLocation(int restaurantId)
        {
            var locate = await _dbContext.Restaurant.Where(r => r.ID == restaurantId).Select(l => l.Location).FirstAsync();
            return locate;
        }
        
        //get restaurants as per location
        public async Task<ICollection<Restaurant>> GetRestaurantsForLocation(int locationID)
        {
            List<Restaurant> restaurants = new List<Restaurant>();
            List<Location> locations = new List<Location>();
            var restaurant = await _dbContext.Location.Where(r => r.ID == locationID).ToListAsync();
            return ;
        }

        //get all restaurants
        public async Task<ICollection<Restaurant>> GetRestaurants()
        {
            var allRestaurants = await _dbContext.Restaurant.ToListAsync();
            return allRestaurants;
        }

        //get restaurants as per user Id
        public async Task<Restaurant> GetUserRestaurants(int userId)
        {
            var items = await _dbContext.Restaurant.Where(r => r.ID == userId).Include(d => d.Dishes).FirstAsync();
            return items;
        }

        //POST
        //post all locations
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
        }

        //post all restaurants
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
        }

        //post users and create order details
        public async Task<OrderDetailsAC> AddOrderDetails(OrderDetailsAC detailsAC)
        {
            Restaurant restaurants = await _dbContext.Restaurant.FirstAsync(x => x.ID == detailsAC.RestaurantID);
            OrderDetails orders = new OrderDetails();
            List<DishesOrdered> dishes = new List<DishesOrdered>();
            List<Dishes> orderedDishes = await _dbContext.Dishes.Where(x => detailsAC.DishesName.Contains(x.ID)).ToListAsync();
            foreach (var item in orderedDishes)
            {
                dishes.Add(new DishesOrdered {
                    Dishes = item
                });
            }
            foreach (var item in detailsAC)
            {
                List<User> user = new List<User>();
                foreach (var elements in item.UserName)
                {
                    user.Add(new User
                    {
                        Name = elements
                    });
                }
            }
            //foreach (var item in detailsAC)
            //{
            //    foreach (var elements in item.RestaurantID)
            //    {
            //        restaurants.Add(await _dbContext.Restaurant.FirstAsync(x => x.ID == elements));
            //    }
            //    List<User> user = new List<User>();
            //    foreach (var elements in item.UserName)
            //    {
            //        user.Add(new User
            //        {
            //            Name = elements
            //        });
            //    }
            //    foreach (var elements in item.UserID)
            //    {
            //        orders.Add(new OrderDetails
            //        {
            //            Restaurant = restaurants.Add(await _dbContext.DishesOrdered.FirstAsync(d => d.ID))
            //        });
            //    }   
            //}
            //include sum of dishes' cost
            _dbContext.Restaurant.AddRange(restaurants);
            await _dbContext.SaveChangesAsync();
            return (detailsAC);
        }

        //PUT  
        public async Task<RestaurantAC> EditRestaurant(int restaurantId, [FromBody] RestaurantAC restaurantac)
        {
            var edit = await _dbContext.Restaurant.Where(x => x.ID == restaurantId).Include(Location)
           
            _dbContext.Restaurant.UpdateRange(restaurants);
            await _dbContext.SaveChangesAsync();
            return ;
           // throw new NotImplementedException();
        }

        //delete
        public async Task<bool> DeleteRestaurant(int restaurantId)
        {
            var removed = false;
            Restaurant restaurant = await _dbContext.Restaurant.FindAsync(restaurantId);
            if (restaurant != null)
            {
                removed = true;
                _dbContext.Restaurant.Remove(restaurant);
            }
            await _dbContext.SaveChangesAsync();
            return removed;
            //throw new NotImplementedException();
        }
    }
}
