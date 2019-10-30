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
        //get all the details as per restaurant Id : user
        public async Task<ICollection<AllDetails>> GetRestaurantLocation(int id)
        {
            var eatery = await _dbContext.Restaurant.Include(l => l.Location).Where(r => r.ID == id).SelectMany(l => l.Location).ToListAsync();
            var cityList = await _dbContext.City.ToListAsync();
            var restaurantList = await _dbContext.Restaurant.ToListAsync();
            var countryList = await _dbContext.Country.ToListAsync();
            var x = restaurantList.Where(d => d.ID == id).FirstOrDefault();

            var allLocations = new List<AllDetails>();
            foreach (var place in eatery)
            {
                allLocations.Add(new AllDetails
                {
                    ID = x.ID,
                    LocationID = place.ID,
                    Locality = place.Locality,
                    RestaurantName = x.RestaurantName,
                    City = new AllCity
                    {
                        ID = place.CityID,
                        Name = cityList.Where(c => c.ID == place.CityID).Select(c => c.CityName).FirstOrDefault()
                    },
                    Country = new AllCountry
                    {
                        ID = place.CountryID,
                        Name = countryList.Where(c => c.ID == place.CountryID).Select(c => c.CountryName).FirstOrDefault()
                    },
                    ContactNumber = x.ContactNumber,
                    CuisineType = x.CuisineType,
                    AverageCost = x.AverageCost,
                    OpeningHours = x.OpeningHours,
                    MoreInfo = x.MoreInfo
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
                    RestaurantName = item.RestaurantName,
                    Description = item.Description
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

        //get dishes for restaurant : user
        public async Task<ICollection<AllDishes>> GetDishes(int restaurantId)
        {
            var dish = await _dbContext.Restaurant.Include(d => d.Dishes).Where(r => r.ID == restaurantId).Select(e => e.Dishes).SingleAsync();
            var allDishes = new List<AllDishes>();
            foreach (var eatery in dish)
            {
                allDishes.Add(new AllDishes
                {
                    ID = eatery.ID,
                    DishesName = eatery.DishesName,
                    Costs = eatery.Costs
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
        public async Task<AllDetails> AddAllRestaurants(AllDetails details)
        {

            City city = new City();
            city.CityName = details.City.Name;

            var z = await _dbContext.City.AddAsync(city);

            Country country = new Country();
            country.CountryName = details.Country.Name;

            var w = await _dbContext.Country.AddAsync(country);

            Location location = new Location();
            location.Locality = details.Locality;
            location.CityID = city.ID;
            location.CountryID = country.ID;

            Restaurant restaurants = new Restaurant();
            restaurants.RestaurantName = details.RestaurantName;
            restaurants.Description = details.Description;
            restaurants.ContactNumber = details.ContactNumber;
            restaurants.CuisineType = details.CuisineType;
            restaurants.AverageCost = details.AverageCost;
            restaurants.OpeningHours = details.OpeningHours;
            restaurants.MoreInfo = details.MoreInfo;

            ICollection<Location> Location = new List<Location>();
            Location.Add(location);
            ICollection<Dishes> Dishes = new List<Dishes>();

            restaurants.Location = Location;
            restaurants.Dishes = Dishes;


            await _dbContext.Restaurant.AddAsync(restaurants);

            await _dbContext.Location.AddAsync(location);

            await _dbContext.SaveChangesAsync();
            return (details);
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

        //post all dishes
        public async Task<AllDishes> NewDish(int restaurantId, AllDishes dishes)
        {
            var menu = await _dbContext.Restaurant.Include(d => d.Dishes).Where(r => r.ID == restaurantId).FirstAsync();
            Dishes dish = new Dishes();
            dish.DishesName = dishes.DishesName;
            dish.Costs = dishes.Costs;

            menu.Dishes.Add(dish);

            await _dbContext.SaveChangesAsync();
            return (dishes);
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
        }

        //update restaurant details(dishes/ location) : admin
        public async Task<AllDetails> EditRestaurant(int id,AllDetails details)
        {
            City city = new City();
            city.CityName = details.City.Name;
            city.ID = details.City.ID;

             _dbContext.Entry(city).State = EntityState.Modified;

            Country country = new Country();
            country.CountryName = details.Country.Name;
            country.ID = details.Country.ID;

            _dbContext.Entry(country).State = EntityState.Modified;

            Location location = new Location();
            location.ID = details.LocationID;
            location.Locality = details.Locality;
            location.CityID = city.ID;
            location.CountryID = country.ID;

            Restaurant restaurants = new Restaurant();
            restaurants.RestaurantName = details.RestaurantName;
            restaurants.Description = details.Description;
            restaurants.ContactNumber = details.ContactNumber;
            restaurants.CuisineType = details.CuisineType;
            restaurants.AverageCost = details.AverageCost;
            restaurants.OpeningHours = details.OpeningHours;
            restaurants.MoreInfo = details.MoreInfo;
            restaurants.ID = id;

            ICollection<Location> Location = new List<Location>();
            Location.Add(location);
            ICollection<Dishes> Dishes = new List<Dishes>();

            restaurants.Location = Location;
            restaurants.Dishes = Dishes;

            _dbContext.Entry(restaurants).State = EntityState.Modified;
            _dbContext.Entry(location).State = EntityState.Modified;


            await _dbContext.SaveChangesAsync();
            return details;
        }

        //DELETE
        //delete all restaurants : admin
        public async Task DeleteRestaurant(int restaurantId)
        {
            var restaurant = await _dbContext.Restaurant.FindAsync(restaurantId);
            if (restaurant != null)
            {
                _dbContext.Restaurant.Remove(restaurant);
            }
            await _dbContext.SaveChangesAsync();
            return;
        }

        //delete dishes
        public async Task DeleteDishes(int id)
        {
            var dish = await _dbContext.Dishes.FindAsync(id);
            if (dish != null)
            {
                _dbContext.Dishes.Remove(dish);
            }
            await _dbContext.SaveChangesAsync();
            return;
        }
    }
}
