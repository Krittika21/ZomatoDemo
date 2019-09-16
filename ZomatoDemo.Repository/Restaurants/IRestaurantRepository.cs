using System;
using System.Collections.Generic;
using System.Text;
using ZomatoDemo.DomainModel.Models;

namespace ZomatoDemo.Repository.Restaurants
{
    public interface IRestaurantRepository
    {
        //get
        Location GetRestaurantLocation(long restaurantId);
        IEnumerable<Restaurant> GetRestaurants();
        IEnumerable<Restaurant> GetUserRestaurants(long userId, long restaurantId);

        //post
        object AddLocation(Location location);
        object AddAllRestaurants(Restaurant restaurants);

        //edit
        Restaurant EditRestaurant(long restaurantId);

        //delete
        bool DeleteRestaurant(long restaurantId);
    }
}
