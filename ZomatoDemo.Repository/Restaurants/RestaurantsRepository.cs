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

        //private ZomatoDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IDataRepository _dataRepository;

        public RestaurantsRepository(IMapper mapper, IDataRepository dataRepository)
        {
            _mapper = mapper;
            _dataRepository = dataRepository;
        }

        //GET
        //get all the details as per restaurant Id : user

        public async Task<AllDetails> GetRestaurantDetails(int id)
        {
            var eatery = await _dataRepository.Where<Restaurant>(r => r.ID == id).Include(l => l.Location).FirstAsync();
            var reviews = await _dataRepository.Where<Review>(r => r.Restaurant.ID == id).Include(u => u.User).ToListAsync();
            var comments = await _dataRepository.GetAll<Comment>().Include(c => c.User).ToListAsync();

            //reviews
            var reviewsAC = new List<ReviewsAC>();
            _mapper.Map(reviews, reviewsAC);

            //comments
            var commentACs = new List<CommentAC>();
            List<CommentAC> comment = new List<CommentAC>(); 
            foreach (var item in reviewsAC)
            {
                var allComments = comments.Where(k => k.ReviewID == item.ReviewId).ToList();
                _mapper.Map(allComments, commentACs);
                comment.AddRange(commentACs);
            }
            //adding every detail to AllDetails class
            var details = new AllDetails
            {
                RestaurantID = id,
                LocationID = eatery.ID,
                Locations = eatery.Location,
                RestaurantName = eatery.RestaurantName,
                Description = eatery.Description,
                ContactNumber = eatery.ContactNumber,
                CuisineType = eatery.CuisineType,
                AverageCost = eatery.AverageCost,
                OpeningHours = eatery.OpeningHours,
                MoreInfo = eatery.MoreInfo,
                Reviews = reviewsAC,
                Comments = comment
            };

            //location
            var allCity = new AllCity();
            var allCountry = new AllCountry();

            _mapper.Map(await _dataRepository.Where<City>(c => c.ID == eatery.Location.FirstOrDefault().CityID).FirstAsync(), allCity);
            _mapper.Map(await _dataRepository.Where<Country>(c => c.ID == eatery.Location.FirstOrDefault().CountryID).FirstAsync(), allCountry);

            details.City = allCity;
            details.Country = allCountry;

            return details;
        }

        //get all restaurants : admin

        public async Task<ICollection<AllRestaurants>> GetAllRestaurants()
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
            await _dataRepository.AddRangeAsync<Location>(locations);
            await _dataRepository.SaveChangesAsync();
            return (locationAC);
        }

        //post all restaurants
        public async Task<AllDetails> AddAllRestaurants(AllDetails details)
        {

            City city = new City();
            _mapper.Map(details.City, city);
            await _dataRepository.AddAsync<City>(city);

            Country country = new Country();
            _mapper.Map(details.Country, country);
            await _dataRepository.AddAsync<Country>(country);

            Location location = new Location();
            _mapper.Map(details, location);
            await _dataRepository.AddAsync<Location>(location);

            Restaurant restaurant = new Restaurant();
            _mapper.Map(details, restaurant);
            await _dataRepository.AddAsync<Restaurant>(restaurant);

            ICollection<Location> Locations = new List<Location>();
            Locations.Add(location);
            ICollection<Dishes> Dishes = new List<Dishes>();

            restaurant.Location = Locations;
            restaurant.Dishes = Dishes;

            await _dataRepository.AddAsync<Location>(location);
            await _dataRepository.SaveChangesAsync();
            return (details);
        }

        //post users and create order details
        public async Task<OrderDetailsAC> AddOrderDetails(OrderDetailsAC detailsAC)
        {
            Restaurant restaurants = await _dataRepository.FirstAsync<Restaurant>(x => x.ID == detailsAC.RestaurantID);
            User user = await _dataRepository.Where<User>(u => u.Id == detailsAC.UserID).FirstAsync();
            ICollection<DishesOrdered> orderedDishes = detailsAC.DishesOrdered;
            var listOfDishes = _dataRepository.GetAll<Dishes>();
            foreach (var item in orderedDishes)
            {
                item.Dishes = await listOfDishes.Where(k => k.ID == item.Dishes.ID).FirstAsync();
            }
            OrderDetails orders = new OrderDetails();
            _mapper.Map(detailsAC, user);
            _mapper.Map(detailsAC, restaurants);

            orders.User = user;
            orders.Restaurant = restaurants;

            await _dataRepository.AddAsync<OrderDetails>(orders);
            await _dataRepository.AddRangeAsync<DishesOrdered>(orderedDishes);
            await _dataRepository.SaveChangesAsync();
            detailsAC.DishesOrdered = orderedDishes;
            return detailsAC;
        }

        //post all dishes
        public async Task<AllDishes> NewDish(int restaurantId, AllDishes dishes)
        {
            var menu = await _dataRepository.Where<Restaurant>(r => r.ID == restaurantId).Include(d => d.Dishes).FirstAsync();
            Dishes dish = new Dishes();
            _mapper.Map(dishes, dish);
            await _dataRepository.AddAsync<Dishes>(dish);
            menu.Dishes.Add(dish);
            await _dataRepository.SaveChangesAsync();
            return dishes;
        }

        //post reviews
        public async Task<ReviewsAC> AddReviews(int restaurantId, ReviewsAC reviews)
        {
            Review review = new Review();
            _mapper.Map(reviews, review);
            review.Restaurant = await _dataRepository.Where<Restaurant>(r => r.ID == restaurantId).FirstAsync();
            review.User = await _dataRepository.Where<User>(u => u.Id == reviews.userID).FirstAsync();
            await _dataRepository.AddAsync<Review>(review);
            await _dataRepository.SaveChangesAsync();
            return _mapper.Map(review, reviews);
        }

        //post for likes
        public async Task<ReviewsAC> Likes(int reviewId, string userId)
        {
            var review = await _dataRepository.Where<Review>(k => k.ID == reviewId).Include(k => k.User).FirstOrDefaultAsync();
            var user = await _dataRepository.Where<User>(k => k.Id == userId).FirstOrDefaultAsync();
            var likes = new Likes();
            likes.Reviews = review;
            likes.Users = user;
            review.LikesCount += 1;
            await _dataRepository.AddAsync(likes);

            ReviewsAC reviews = new ReviewsAC();
            _mapper.Map(review, reviews);

            await _dataRepository.SaveChangesAsync();
            return reviews;
        }

        //post comments
        public async Task<CommentAC> CommentSection(int restaurantId, CommentAC commentac)
        {
            Comment comment = new Comment();
            var x = _dataRepository.Where<Restaurant>(r => r.ID == restaurantId);
            var z = await x.FirstAsync();
            var y = await _dataRepository.Where<Review>(r => r.ID == commentac.ReviewID).FirstAsync();
            _mapper.Map(commentac, comment);

            await _dataRepository.AddAsync<Comment>(comment);
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
            _dataRepository.Entry(city);

            Country country = new Country();
            _mapper.Map(details.Country, country);
            _dataRepository.Entry(country);

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

            _dataRepository.Entry(location);
            _dataRepository.Entry(restaurants);


            await _dataRepository.SaveChangesAsync();
            return details;
        }

        //________________________________________________________________________________________________________________________________
        //DELETE
        //delete all restaurants : admin
        public async Task DeleteRestaurant(int restaurantId)
        {
            var delRestaurant = await _dataRepository.Where<Restaurant>(r => r.ID == restaurantId).Include(k => k.Dishes).SingleOrDefaultAsync();
            var delOrderDetails = await _dataRepository.Where<OrderDetails>(k => k.Restaurant.ID == restaurantId).Include(r => r.Restaurant).Include(l => l.DishesOrdered).ToListAsync();
            var delLike = await _dataRepository.Where<Likes>(r => r.Reviews.Restaurant.ID == restaurantId).Include(r => r.Reviews).ToListAsync();
            var delComments = await _dataRepository.Where<Comment>(r => r.Review.Restaurant.ID == restaurantId).Include(t => t.Review).ToListAsync();
            var reviews = await _dataRepository.Where<Review>(k => k.Restaurant.ID == restaurantId).Include(r => r.Restaurant).ToListAsync();

            _dataRepository.RemoveRange<Review>(reviews);
            foreach (var item in delOrderDetails)
            {
                _dataRepository.RemoveRange<DishesOrdered>(item.DishesOrdered);
            }
            _dataRepository.RemoveRange<Comment>(delComments);
            _dataRepository.RemoveRange<Likes>(delLike);
            _dataRepository.RemoveRange<OrderDetails>(delOrderDetails);
            _dataRepository.RemoveRange<Dishes>(delRestaurant.Dishes);
            if (delRestaurant != null)
            {
                _dataRepository.Remove<Restaurant>(delRestaurant);
            }
            await _dataRepository.SaveChangesAsync();
            return;
        }

        public async Task DeleteDishes(int id)
        {
            var dish = await _dataRepository.FindAsyncById<Dishes>(id);
            var delDishesOrdered = await _dataRepository.Where<DishesOrdered>(r => r.Dishes.ID == id).Include(d => d.Dishes).ToListAsync();
            var orderDetails = await _dataRepository.GetAll<OrderDetails>().Include(k => k.DishesOrdered).ToListAsync();
            var delOrderDetails = new List<OrderDetails>();

            if (dish != null)
            {
                _dataRepository.Remove<Dishes>(dish);
            }
            if (delDishesOrdered != null)
            {
                _dataRepository.RemoveRange<DishesOrdered>(delDishesOrdered);
            }

            foreach (var item in delDishesOrdered)
            {
                var x = orderDetails.Where(k => k.DishesOrdered.Contains(item)).FirstOrDefault();
                delOrderDetails.Add(x);

            }
            _dataRepository.RemoveRange<OrderDetails>(delOrderDetails);
            await _dataRepository.SaveChangesAsync();
            return;
        }

        //delete review

    }
}
