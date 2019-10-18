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
        //get locations as per restaurant Id : user
        public async Task<ICollection<AllLocations>> GetRestaurantLocation(int restaurantId)
        {
            var eatery = await _dbContext.Restaurant.Where(r => r.ID == restaurantId).SelectMany(l => l.Location).ToListAsync();
            var allLocations = new List<AllLocations>();
            foreach (var place in eatery)
            {
                allLocations.Add(new AllLocations
                {
                    ID = place.ID,
                    Locality = place.Locality,
                    City = new AllCity
                    {
                        ID = place.City.ID,
                        Name = place.City.CityName
                    },
                    Country = new AllCountry
                    {
                        ID = place.Country.ID,
                        Name = place.Country.CountryName
                    }
                });
            }
            return allLocations;
        }

        //get restaurants as per location : user   for every location add restaurants
        public async Task<ICollection<AllRestaurants>> GetRestaurantsForLocation(int locationID)
        {
            Location locations = await _dbContext.Location.FirstAsync(x => x.ID == locationID);
            var restaurant = await _dbContext.Restaurant.Where(r => r.Location.Contains(locations)).ToListAsync();
            var allRestaurants = new List<AllRestaurants>();
            foreach (var place in restaurant)
            {
                allRestaurants.Add(new AllRestaurants
                {
                    ID = place.ID,
                    RestaurantName = place.RestaurantName
                });
            }
            return allRestaurants;
        }

        //get all restaurants : admin
        public async Task<ICollection<AllRestaurants>> GetRestaurants()
        {
            var restaurants = await _dbContext.Restaurant.ToListAsync();
            var allRestaurants = new List<AllRestaurants>();
            foreach (var item in restaurants)
            {
                allRestaurants.Add(new AllRestaurants
                {
                    ID = item.ID,
                    RestaurantName = item.RestaurantName
                });
            }
            return allRestaurants;
        }

        //get restaurants as per user Id : restaurant user
        public async Task<AllRestaurants> GetUserRestaurants(int userId)
        {
            var items = await _dbContext.Restaurant.Where(r => r.ID == userId).Include(d => d.Dishes).FirstAsync();
            var allRestaurants = new List<AllRestaurants>();
            allRestaurants.Add(new AllRestaurants
            {
                ID = items.ID,
                RestaurantName = items.RestaurantName
            });
            return allRestaurants.SingleOrDefault(r=>r.ID == items.ID);
        }

        //get dishes for cart : user
        public async Task<ICollection<AllDishes>> GetDishes(int restaurantId)
        {
            var dish = await _dbContext.Dishes.Where(r => r.ID == restaurantId).ToListAsync();
            var allDishes = new List<AllDishes>();
            foreach (var eatery in dish)
            {
                allDishes.Add(new AllDishes
                {
                    ID = eatery.ID,
                    DishesName = eatery.DishesName
                });
            }            
            return allDishes;
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
        public async Task AddOrderDetails(OrderDetailsAC detailsAC)
        {
            Restaurant restaurants = await _dbContext.Restaurant.FirstAsync(x => x.ID == detailsAC.RestaurantID);
            OrderDetails orders = new OrderDetails();
            List<DishesOrdered> dishes = new List<DishesOrdered>();
            List<Dishes> orderedDishes = await _dbContext.Dishes.Where(x => detailsAC.DishesID.Contains(x.ID)).ToListAsync();
            foreach (var item in orderedDishes)
            {
                dishes.Add(new DishesOrdered {
                    Dishes = item
                });
            }
            orders.Restaurant = restaurants;
            orders.DishesOrdered = dishes;
            _dbContext.OrderDetails.Add(orders);
            await _dbContext.SaveChangesAsync();
        }

        //PUT 
        // update cart dishes : user
        public async Task<bool> EditCart(int orderId, [FromBody] OrderDetailsAC orderDetailsac)
        {
            var removed = false;
            var edit = await _dbContext.OrderDetails.Where(x => x.ID == orderId).Include(d => d.DishesOrdered).FirstAsync(); 
            if (edit.DishesOrdered != null)
            {
                removed = true;
                edit.DishesOrdered.Remove(edit.DishesOrdered.First(x => x.ID == orderDetailsac.DishesID.First()));
                await _dbContext.SaveChangesAsync();
            } 
            return removed;
           // throw new NotImplementedException();
        }
        //update restaurant details(dishes/ location) : admin

        //DELETE
        //delete all restaurants : admin
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
