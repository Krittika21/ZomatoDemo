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
        Task<ICollection<AllDetails>> GetRestaurantLocation(int restaurantId);
        Task<ICollection<AllRestaurants>> GetRestaurantsForLocation(int locationID);
        Task<ICollection<AllRestaurants>> GetRestaurants();
        Task<AllRestaurants> GetUserRestaurants(int userId);
        Task<ICollection<AllDishes>> GetDishes(int restaurantId);

        //post
        Task<IEnumerable<LocationAC>> AddLocation([FromBody] List<LocationAC> locationAC);
        Task AddOrderDetails(OrderDetailsAC detailsAC);
        Task<AllDetails> AddAllRestaurants(AllDetails x);

        //edit
        Task<bool> EditCart(int orderId, [FromBody] OrderDetailsAC orderDetailsac);
        Task<AllDetails> EditRestaurant(int id, AllDetails details);

        //delete
        Task<bool> DeleteRestaurant(int id);
    }
}
