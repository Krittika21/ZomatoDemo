using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Application_Classes;
using ZomatoDemo.DomainModel.Models;

namespace ZomatoDemo.Repository.Restaurants
{
    public interface IRestaurantRepository
    {
        //get
        Location GetRestaurantLocation(int restaurantId);
        IEnumerable<Restaurant> GetRestaurants();
        IEnumerable<Restaurant> GetUserRestaurants(int userId, int restaurantId);

        //post
        Task<IEnumerable<LocationAC>> AddLocation(List<LocationAC> location);
        IEnumerable<Restaurant> AddAllRestaurants(List<Restaurant> restaurants);

        //edit
        Restaurant EditRestaurant([FromBody] List<Restaurant> restaurants);

        //delete
        bool DeleteRestaurant(int restaurantId);
    }
}
