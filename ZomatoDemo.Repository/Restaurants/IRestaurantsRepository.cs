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
        Task<Location> GetRestaurantLocation(int restaurantId);
        Task<IEnumerable<Restaurant>> GetRestaurants();
        Task<Restaurant> GetUserRestaurants(int userId);

        //post
        Task<IEnumerable<LocationAC>> AddLocation(List<LocationAC> location);
        Task<IEnumerable<RestaurantAC>> AddAllRestaurants(List<RestaurantAC> restaurants);

        //edit
        Task<Restaurant> EditRestaurant([FromBody] List<Restaurant> restaurants);

        //delete
        Task<bool> DeleteRestaurant(int restaurantId);
    }
}
