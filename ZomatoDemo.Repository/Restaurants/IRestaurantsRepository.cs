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
        Task<ICollection<AllLocations>> GetRestaurantLocation(int restaurantId);
        Task<ICollection<AllRestaurants>> GetRestaurantsForLocation(int locationID);
        Task<ICollection<AllRestaurants>> GetRestaurants();
        Task<AllRestaurants> GetUserRestaurants(int userId);
        Task<ICollection<AllDishes>> GetDishes(int restaurantId);

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
