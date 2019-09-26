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
        Task<ICollection<Location>> GetRestaurantLocation(int restaurantId);
        Task<ICollection<Restaurant>> GetRestaurantsForLocation(int locationID);
        Task<ICollection<Restaurant>> GetRestaurants();
        Task<Restaurant> GetUserRestaurants(int userId);
        Task<ICollection<Dishes>> GetDishes(int restaurantId);

        //post
        Task<IEnumerable<LocationAC>> AddLocation([FromBody] List<LocationAC> locationAC);
        Task<IEnumerable<RestaurantAC>> AddAllRestaurants(List<RestaurantAC> restaurants);
        Task AddOrderDetails(OrderDetailsAC detailsAC);

        //edit
        Task<bool> EditCart(int orderId, [FromBody] OrderDetailsAC orderDetailsac);

        //delete
        Task<bool> DeleteRestaurant(int restaurantId);
    }
}
