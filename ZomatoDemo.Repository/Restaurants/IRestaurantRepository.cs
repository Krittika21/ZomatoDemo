using System;
using System.Collections.Generic;
using System.Text;
using ZomatoDemo.DomainModel.Models;

namespace ZomatoDemo.Repository.Restaurants
{
    public interface IRestaurantRepository
    {
        object AddLocation(Location location);
        object AddRestaurant(Restaurant restaurants);
        //Restaurant GetRestaurant(long id);
        IEnumerable<Restaurant> GetRestaurants();
        Restaurant EditRestaurant(long restaurantId);
        bool DeleteRestaurant(long restaurantId);
        IEnumerable<Restaurant> GetUserRestaurants(long userId);
        object AddRestaurantToUser(long userId, long restaurantId);
    }
}
