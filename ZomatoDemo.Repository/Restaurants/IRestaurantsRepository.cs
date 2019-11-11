using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Application_Classes;
using ZomatoDemo.DomainModel.Models;

namespace ZomatoDemo.Repository.Restaurants
{
    public interface IRestaurantsRepository
    {
        //get
        Task<AllDetails> GetRestaurantLocation(int restaurantId);
        Task<ICollection<AllRestaurants>> GetRestaurantsForLocation(int locationID);
        Task<ICollection<AllRestaurants>> GetRestaurants();
        Task<AllRestaurants> GetUserRestaurants(int userId);
        Task<ICollection<AllDishes>> GetDishes(int restaurantId);

        //post
        Task<IEnumerable<LocationAC>> AddLocation([FromBody] List<LocationAC> locationAC);
        Task AddOrderDetails(OrderDetailsAC detailsAC);
        Task<AllDetails> AddAllRestaurants(AllDetails x);
        Task<AllDishes> NewDish(int restaurantId, AllDishes dishes);
        Task<ReviewsAC> AddReviews(int restaurantId, ReviewsAC reviews);
        Task<ReviewsAC> Likes(int reviewId, string userId);
        Task<CommentAC> CommentSection(int restaurantId, CommentAC commentac);

        //edit
        Task<bool> EditCart(int orderId, [FromBody] OrderDetailsAC orderDetailsac);
        Task<AllDetails> EditRestaurant(int id, AllDetails details);

        //delete
        Task DeleteRestaurant(int id);
        Task DeleteDishes(int id);
    }
}
