using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Application_Classes;
using ZomatoDemo.DomainModel.Models;
using ZomatoDemo.Web.Models;

namespace ZomatoDemo.Repository.Restaurants
{
    public class RestaurantsRepository : IRestaurantsRepository
    {

        private ZomatoDbContext _dbContext;
        private readonly IMapper _mapper;

        public RestaurantsRepository(ZomatoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        //GET
        //get all the details as per restaurant Id : user

        public async Task<AllDetails> GetRestaurantLocation(int id)
        {
            var details = new AllDetails();
            var eatery = await _dbContext.Restaurant.Include(l => l.Location).Where(r => r.ID == id).SelectMany(l => l.Location).FirstAsync();
            var restaurant = await _dbContext.Restaurant.Include(l => l.Location).Where(r => r.ID == id).FirstOrDefaultAsync();
            var country = await _dbContext.Country.Where(c => c.ID == restaurant.Location.FirstOrDefault().CountryID).FirstAsync();
            var city = await _dbContext.City.Where(c => c.ID == restaurant.Location.FirstOrDefault().CityID).FirstAsync();

            details.LocationID = eatery.ID;
            restaurant.ID = id;
            var allCity = new AllCity();
            var allCountry = new AllCountry();
            var reviewsX = new List<ReviewsAC>();
            var reviewUser = await _dbContext.Review.Where(r => r.Restaurant.ID == id).Include(u => u.User).ToListAsync();

            _mapper.Map(eatery, details);
            _mapper.Map(city, allCity);
            details.City = allCity;
            _mapper.Map(country, allCountry);
            details.Country = allCountry;
            _mapper.Map(restaurant, details);
            _mapper.Map(country, details.Country);
            _mapper.Map(reviewUser, reviewsX);

            var comments = await _dbContext.Comment.ToListAsync();
            var xx = new List<CommentAC>();
            var y = new List<CommentAC>();
            _mapper.Map(comments, y);
            foreach (var item in reviewsX)
            {
                var cmnts = comments.Where(k => k.ReviewID == item.ReviewId).ToList();
                _mapper.Map(cmnts, xx);
                item.commentACs = xx;
            }
            details.AllReviews = reviewsX;

            return details;
        }

        //get restaurants as per location : user   for every location add restaurants
        [Authorize(Policy = "ApiUser")]
        public async Task<ICollection<AllRestaurants>> GetRestaurantsForLocation(int locationID)
        {
            Location locations = await _dbContext.Location.FirstAsync(x => x.ID == locationID);
            var restaurant = await _dbContext.Restaurant.Where(r => r.Location.Contains(locations)).ToListAsync();
            var allRestaurants = new List<AllRestaurants>();
            _mapper.Map(restaurant, allRestaurants);
            return allRestaurants;
        }

        //get all restaurants : admin
        public async Task<ICollection<AllRestaurants>> GetRestaurants()
        {
            var restaurants = await _dbContext.Restaurant.ToListAsync();
            var allRestaurants = new List<AllRestaurants>();
            _mapper.Map(restaurants, allRestaurants);
            return allRestaurants;
        }

        //get restaurants as per user Id : restaurant user
        public async Task<AllRestaurants> GetUserRestaurants(int userId)
        {
            var items = await _dbContext.Restaurant.Where(r => r.ID == userId).Include(d => d.Dishes).FirstAsync();
            var allRestaurants = new List<AllRestaurants>();
            _mapper.Map(items, allRestaurants);
            return allRestaurants.SingleOrDefault(r => r.ID == items.ID);
        }

        //get dishes for restaurant : user
        public async Task<ICollection<AllDishes>> GetDishes(int restaurantId)
        {
            var dish = await _dbContext.Restaurant.Include(d => d.Dishes).Where(r => r.ID == restaurantId).Select(e => e.Dishes).SingleAsync();
            var allDishes = new List<AllDishes>();
            _mapper.Map(dish, allDishes);
            return allDishes;
        }

        //________________________________________________________________________________________________________________________________
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
            _mapper.Map(details.City, city);

            var z = await _dbContext.City.AddAsync(city);

            Country country = new Country();
            _mapper.Map(details.Country, country);

            var w = await _dbContext.Country.AddAsync(country);

            Location location = new Location();
            _mapper.Map(details, location);

            Restaurant restaurants = new Restaurant();
            _mapper.Map(details, restaurants);

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
        public async Task<OrderDetailsAC> AddOrderDetails(OrderDetailsAC detailsAC)
        {
            Restaurant restaurants = await _dbContext.Restaurant.FirstAsync(x => x.ID == detailsAC.RestaurantID);
            User user = await _dbContext.Users.Where(u => u.Id == detailsAC.UserID).FirstAsync();
            ICollection<DishesOrdered> orderedDishes = detailsAC.DishesOrdered;
            var listOfDishes = _dbContext.Dishes;
            foreach (var item in orderedDishes)
            {
                item.Dishes = await listOfDishes.Where(k => k.ID == item.Dishes.ID).FirstAsync();
            }
            OrderDetails orders = new OrderDetails();
            _mapper.Map(detailsAC, user);
            _mapper.Map(detailsAC, restaurants);            

            orders.User = user;
            orders.Restaurant = restaurants;
            _dbContext.OrderDetails.Add(orders);
            //await _dbContext.AddAsync(user);
            //await _dbContext.AddAsync(restaurants);
            await _dbContext.AddRangeAsync(orderedDishes);

            await _dbContext.SaveChangesAsync();
            detailsAC.DishesOrdered = orderedDishes;
            return detailsAC;
        }

        //post all dishes
        public async Task<AllDishes> NewDish(int restaurantId, AllDishes dishes)
        {
            var menu = await _dbContext.Restaurant.Include(d => d.Dishes).Where(r => r.ID == restaurantId).FirstAsync();
            Dishes dish = new Dishes();
            _mapper.Map(dishes, dish);
            await _dbContext.Dishes.AddAsync(dish);
            menu.Dishes.Add(dish);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map(dish, dishes);
        }

        //post reviews
        public async Task<ReviewsAC> AddReviews(int restaurantId, ReviewsAC reviews)
        {
            Review review = new Review();
            _mapper.Map(reviews, review);
            review.Restaurant = await _dbContext.Restaurant.Where(r => r.ID == restaurantId).FirstAsync();
            review.User = await _dbContext.Users.Where(u => u.Id == reviews.userID).FirstAsync();
            await _dbContext.Review.AddAsync(review);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map(review, reviews);
        }

        //post for likes
        public async Task<ReviewsAC> Likes(int reviewId, string userId)
        {
            var review = await _dbContext.Review.Where(k => k.ID == reviewId).Include(k => k.User).FirstOrDefaultAsync();
            var user = await _dbContext.Users.Where(k => k.Id == userId).FirstOrDefaultAsync();
            var likes = new Likes();
            likes.Reviews = review;
            likes.Users = user;
            review.LikesCount += 1;
            await _dbContext.Likes.AddAsync(likes);

            ReviewsAC reviews = new ReviewsAC();
            _mapper.Map(reviews, review);

            await _dbContext.SaveChangesAsync();
            return reviews;
        }

        //post comments
        public async Task<CommentAC> CommentSection(int restaurantId, CommentAC commentac)
        {
            Comment comment = new Comment();
            var x = await _dbContext.Restaurant.Where(r => r.ID == restaurantId).FirstAsync();
            var y = await _dbContext.Review.Where(r => r.ID == commentac.ReviewID).FirstAsync();
           // _mapper.Map(y, comment);
           // _mapper.Map(x, comment);
            _mapper.Map(commentac, comment);

            await _dbContext.Comment.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
            commentac.ID = comment.ID;
            return commentac;
        }

        //________________________________________________________________________________________________________________________________

        //update restaurant details(dishes/ location) : admin
        public async Task<AllDetails> EditRestaurant(int id, AllDetails details)
        {
            City city = new City();
            _mapper.Map(details.City, city);
            _dbContext.Entry(city).State = EntityState.Modified;

            Country country = new Country();
            _mapper.Map(details.Country, country);
            _dbContext.Entry(country).State = EntityState.Modified;

            Location location = new Location();
            _mapper.Map(details, location);

            Restaurant restaurants = new Restaurant();
            _mapper.Map(details, restaurants);
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

        //________________________________________________________________________________________________________________________________
        //DELETE
        //delete all restaurants : admin
        public async Task DeleteRestaurant(int restaurantId)
        {
            var delRestaurant = await _dbContext.Restaurant.Include(k => k.Dishes).Where(r => r.ID == restaurantId).SingleOrDefaultAsync();
            var delOrderDetails = await _dbContext.OrderDetails.Include(r => r.Restaurant).Include(l => l.DishesOrdered).Where(k => k.Restaurant.ID == restaurantId).ToListAsync();
            var delLike = await _dbContext.Likes.Include(r => r.Reviews).Where(r => r.Reviews.Restaurant.ID == restaurantId).ToListAsync();
            var delComments = await _dbContext.Comment.Include(t => t.Review).Where(r => r.Review.Restaurant.ID == restaurantId).ToListAsync();
            var reviews = await _dbContext.Review.Include(r => r.Restaurant).Where(k => k.Restaurant.ID == restaurantId).ToListAsync();

            _dbContext.Review.RemoveRange(reviews);
            foreach (var item in delOrderDetails)
            {
                _dbContext.DishesOrdered.RemoveRange(item.DishesOrdered);
            }
            _dbContext.Comment.RemoveRange(delComments);
            _dbContext.Likes.RemoveRange(delLike);
            _dbContext.OrderDetails.RemoveRange(delOrderDetails);
            _dbContext.Dishes.RemoveRange(delRestaurant.Dishes);
            if (delRestaurant != null)
            {
                _dbContext.Restaurant.Remove(delRestaurant);
            }
            await _dbContext.SaveChangesAsync();
            return;
        }

        //delete dishes
        public async Task DeleteDishes(int id)
        {
            var dish = await _dbContext.Dishes.FindAsync(id);
            var delDishesOrdered = await _dbContext.DishesOrdered.Include(d => d.Dishes).Where(r => r.Dishes.ID == id).ToListAsync();
            // var delOrderDetails = await _dbContext.OrderDetails.Where(k => k.DishesOrdered == delDishesOrdered).FirstAsync();

            var orderDetails = await _dbContext.OrderDetails.Include(k => k.DishesOrdered).ToListAsync();

            var delOrderDetails = new List<OrderDetails>();

            if (dish != null)
            {
                _dbContext.Dishes.Remove(dish);
            }
            if (delDishesOrdered != null)
            {
                _dbContext.DishesOrdered.RemoveRange(delDishesOrdered);
            }

            foreach (var item in delDishesOrdered)
            {
                var x = orderDetails.Where(k => k.DishesOrdered.Contains(item)).FirstOrDefault();
                delOrderDetails.Add(x);

            }
            _dbContext.OrderDetails.RemoveRange(delOrderDetails);
            await _dbContext.SaveChangesAsync();
            return;
        }

        //delete review

    }
}
