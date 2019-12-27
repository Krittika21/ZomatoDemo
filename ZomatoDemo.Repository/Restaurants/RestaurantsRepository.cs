using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Application_Classes;
using ZomatoDemo.DomainModel.Models;
using ZomatoDemo.Repository.Data_Repository;
using ZomatoDemo.Web.Models;

namespace ZomatoDemo.Repository.Restaurants
{
    public class RestaurantsRepository : IRestaurantsRepository
    {

        private ZomatoDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IDataRepository _dataRepository;
 
        public RestaurantsRepository(ZomatoDbContext dbContext, IMapper mapper, IDataRepository dataRepository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _dataRepository = dataRepository;
        }

        //GET
        //get all the details as per restaurant Id : user

        public async Task<AllDetails> GetRestaurantLocation(int id)
        {
            var details = new AllDetails();
            var eatery = await _dataRepository.Where<Restaurant>(r => r.ID == id).Include(l => l.Location).SelectMany(l => l.Location).FirstAsync();
            var restaurant = await _dataRepository.Where<Restaurant>(r => r.ID == id).Include(l => l.Location).FirstOrDefaultAsync();
            var country = await _dataRepository.Where<Country>(c => c.ID == restaurant.Location.FirstOrDefault().CountryID).FirstAsync();
            var city = await _dataRepository.Where<City>(c => c.ID == restaurant.Location.FirstOrDefault().CityID).FirstAsync();

            details.LocationID = eatery.ID;
            restaurant.ID = id;
            var allCity = new AllCity();
            var allCountry = new AllCountry();
            var reviewsX = new List<ReviewsAC>();
            var reviewUser = await _dataRepository.Where<Review>(r => r.Restaurant.ID == id).Include(u => u.User).ToListAsync();

            _mapper.Map(eatery, details);
            _mapper.Map(city, allCity);
            details.City = allCity;
            _mapper.Map(country, allCountry);
            details.Country = allCountry;
            _mapper.Map(restaurant, details);
            _mapper.Map(country, details.Country);
            _mapper.Map(reviewUser, reviewsX);

            var comments = await _dataRepository.GetAll<Comment>().ToListAsync();
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
            Location locations = await _dataRepository.FirstAsync<Location>(x => x.ID == locationID);
            var restaurant = await _dataRepository.Where<Restaurant>(r => r.Location.Contains(locations)).ToListAsync();
            var allRestaurants = new List<AllRestaurants>();
            _mapper.Map(restaurant, allRestaurants);
            return allRestaurants;
        }

        //get all restaurants : admin
        public async Task<ICollection<AllRestaurants>> GetRestaurants()
        {
            var restaurants = await _dataRepository.GetAll<Restaurant>().ToListAsync();
            var allRestaurants = new List<AllRestaurants>();
            _mapper.Map(restaurants, allRestaurants);
            return allRestaurants;
        }

        //get restaurants as per user Id : restaurant user
        public async Task<AllRestaurants> GetUserRestaurants(int userId)
        {
            var items = await _dataRepository.Where<Restaurant>(r => r.ID == userId).Include(d => d.Dishes).FirstAsync();
            var allRestaurants = new List<AllRestaurants>();
            _mapper.Map(items, allRestaurants);
            return allRestaurants.SingleOrDefault(r => r.ID == items.ID);
        }

        //get dishes for restaurant : user
        public async Task<ICollection<AllDishes>> GetDishes(int restaurantId)
        {
            var dish = await _dataRepository.GetAll<Restaurant>().Include(d => d.Dishes).Where(r => r.ID == restaurantId).Select(e => e.Dishes).SingleAsync();
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
                map = await _dataRepository.GetAll<Country>().FirstAsync(x => x.ID == item.ID);
                locations.Add(new Location
                {
                    City = new City
                    {
                        CityName = item.CityName
                    },
                    Country = map
                });
            }
            await _dbContext.Location.AddRangeAsync(locations);
            await _dataRepository.SaveChangesAsync();
            return (locationAC);
        }

        //post all restaurants
        public async Task<AllDetails> AddAllRestaurants(AllDetails details)
        {

            City city = new City();
            _mapper.Map(details.City, city);

            var z = await _dataRepository.GetAll<City>().AddAsync(city);

            Country country = new Country();
            _mapper.Map(details.Country, country);

            var w = await _dataRepository.GetAll<Country>().AddAsync(country);

            Location location = new Location();
            _mapper.Map(details, location);

            Restaurant restaurants = new Restaurant();
            _mapper.Map(details, restaurants);

            ICollection<Location> Location = new List<Location>();
            Location.Add(location);
            ICollection<Dishes> Dishes = new List<Dishes>();

            restaurants.Location = Location;
            restaurants.Dishes = Dishes;


            await _dataRepository.GetAll<Restaurant>().AddAsync(restaurants);

            await _dataRepository.GetAll<Location>().AddAsync(location);

            await _dataRepository.SaveChangesAsync();
            return (details);
        }

        //post users and create order details
        public async Task<OrderDetailsAC> AddOrderDetails(OrderDetailsAC detailsAC)
        {
            Restaurant restaurants = await _dataRepository.FirstAsync<Restaurant>(x => x.ID == detailsAC.RestaurantID);
            User user = await _dbContext.Users.Where(u => u.Id == detailsAC.UserID).FirstAsync();
            ICollection<DishesOrdered> orderedDishes = detailsAC.DishesOrdered;
            var listOfDishes = _dataRepository.Dishes;
            foreach (var item in orderedDishes)
            {
                item.Dishes = await listOfDishes.Where(k => k.ID == item.Dishes.ID).FirstAsync();
            }
            OrderDetails orders = new OrderDetails();
            _mapper.Map(detailsAC, user);
            _mapper.Map(detailsAC, restaurants);            

            orders.User = user;
            orders.Restaurant = restaurants;
            _dataRepository.OrderDetails.AddSync(orders);
            //await _dbContext.AddAsync(user);
            //await _dbContext.AddAsync(restaurants);
            await _dataRepository.AddRangeAsync(orderedDishes);

            await _dataRepository.SaveChangesAsync();
            detailsAC.DishesOrdered = orderedDishes;
            return detailsAC;
        }

        //post all dishes
        public async Task<AllDishes> NewDish(int restaurantId, AllDishes dishes)
        {
            var menu = await _dataRepository.Restaurant.Include(d => d.Dishes).Where(r => r.ID == restaurantId).FirstAsync();
            Dishes dish = new Dishes();
            _mapper.Map(dishes, dish);
            await _dataRepository.Dishes.AddAsync(dish);
            menu.Dishes.Add(dish);
            await _dataRepository.SaveChangesAsync();
            return _mapper.Map(dish, dishes);
        }

        //post reviews
        public async Task<ReviewsAC> AddReviews(int restaurantId, ReviewsAC reviews)
        {
            Review review = new Review();
            _mapper.Map(reviews, review);
            review.Restaurant = await _dataRepository.Restaurant.Where(r => r.ID == restaurantId).FirstAsync();
            review.User = await _dataRepository.Users.Where(u => u.Id == reviews.userID).FirstAsync();
            await _dataRepository.Review.AddAsync(review);
            await _dataRepository.SaveChangesAsync();
            return _mapper.Map(review, reviews);
        }

        //post for likes
        public async Task<ReviewsAC> Likes(int reviewId, string userId)
        {
            var review = await _dataRepository.Review.Where(k => k.ID == reviewId).Include(k => k.User).FirstOrDefaultAsync();
            var user = await _dataRepository.Users.Where(k => k.Id == userId).FirstOrDefaultAsync();
            var likes = new Likes();
            likes.Reviews = review;
            likes.Users = user;
            review.LikesCount += 1;
            await _dataRepository.Likes.AddAsync(likes);

            ReviewsAC reviews = new ReviewsAC();
            _mapper.Map(reviews, review);

            await _dataRepository.SaveChangesAsync();
            return reviews;
        }

        //post comments
        public async Task<CommentAC> CommentSection(int restaurantId, CommentAC commentac)
        {
            Comment comment = new Comment();
            var x = await _dataRepository.Restaurant.Where(r => r.ID == restaurantId).FirstAsync();
            var y = await _dataRepository.Review.Where(r => r.ID == commentac.ReviewID).FirstAsync();
           // _mapper.Map(y, comment);
           // _mapper.Map(x, comment);
            _mapper.Map(commentac, comment);

            await _dataRepository.Comment.AddAsync(comment);
            await _dataRepository.SaveChangesAsync();
            commentac.ID = comment.ID;
            return commentac;
        }

        //________________________________________________________________________________________________________________________________

        //update restaurant details(dishes/ location) : admin
        public async Task<AllDetails> EditRestaurant(int id, AllDetails details)
        {
            City city = new City();
            _mapper.Map(details.City, city);
            _dataRepository.Entry(city).State = EntityState.Modified;

            Country country = new Country();
            _mapper.Map(details.Country, country);
            _dataRepository.Entry(country).State = EntityState.Modified;

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

            _dataRepository.Entry(location).State = EntityState.Modified;
            _dataRepository.Entry(restaurants).State = EntityState.Modified;


            await _dataRepository.SaveChangesAsync();
            return details;
        }

        //________________________________________________________________________________________________________________________________
        //DELETE
        //delete all restaurants : admin
        public async Task DeleteRestaurant(int restaurantId)
        {
            var delRestaurant = await _dataRepository.Restaurant.Include(k => k.Dishes).Where(r => r.ID == restaurantId).SingleOrDefaultAsync();
            var delOrderDetails = await _dataRepository.OrderDetails.Include(r => r.Restaurant).Include(l => l.DishesOrdered).Where(k => k.Restaurant.ID == restaurantId).ToListAsync();
            var delLike = await _dataRepository.Likes.Include(r => r.Reviews).Where(r => r.Reviews.Restaurant.ID == restaurantId).ToListAsync();
            var delComments = await _dataRepository.Comment.Include(t => t.Review).Where(r => r.Review.Restaurant.ID == restaurantId).ToListAsync();
            var reviews = await _dataRepository.Review.Include(r => r.Restaurant).Where(k => k.Restaurant.ID == restaurantId).ToListAsync();

            _dataRepository.Review.RemoveRange(reviews);
            foreach (var item in delOrderDetails)
            {
                _dataRepository.DishesOrdered.RemoveRange(item.DishesOrdered);
            }
            _dataRepository.Comment.RemoveRange(delComments);
            _dataRepository.Likes.RemoveRange(delLike);
            _dataRepository.OrderDetails.RemoveRange(delOrderDetails);
            _dataRepository.Dishes.RemoveRange(delRestaurant.Dishes);
            if (delRestaurant != null)
            {
                _dataRepository.Restaurant.Remove(delRestaurant);
            }
            await _dataRepository.SaveChangesAsync();
            return;
        }

        //delete dishes
        public async Task DeleteDishes(int id)
        {
            var dish = await _dataRepository.Dishes.FindAsync(id);
            var delDishesOrdered = await _dataRepository.DishesOrdered.Include(d => d.Dishes).Where(r => r.Dishes.ID == id).ToListAsync();
            // var delOrderDetails = await _dbContext.OrderDetails.Where(k => k.DishesOrdered == delDishesOrdered).FirstAsync();

            var orderDetails = await _dataRepository.OrderDetails.Include(k => k.DishesOrdered).ToListAsync();

            var delOrderDetails = new List<OrderDetails>();

            if (dish != null)
            {
                _dataRepository.Dishes.Remove(dish);
            }
            if (delDishesOrdered != null)
            {
                _dataRepository.DishesOrdered.RemoveRange(delDishesOrdered);
            }

            foreach (var item in delDishesOrdered)
            {
                var x = orderDetails.Where(k => k.DishesOrdered.Contains(item)).FirstOrDefault();
                delOrderDetails.Add(x);

            }
            _dataRepository.OrderDetails.RemoveRange(delOrderDetails);
            await _dataRepository.SaveChangesAsync();
            return;
        }

        //delete review

    }
}
