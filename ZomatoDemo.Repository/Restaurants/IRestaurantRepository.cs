using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
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
        object AddLocation(List<Location> location);
        object AddAllRestaurants(List<Restaurant> restaurants);

        //edit
        Restaurant EditRestaurant([FromBody] List<Restaurant> restaurants);

        //delete
        bool DeleteRestaurant(int restaurantId);
    }
}
