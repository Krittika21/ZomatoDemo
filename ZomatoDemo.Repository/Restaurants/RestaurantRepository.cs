using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZomatoDemo.Repository.Restaurants
{
    public class RestaurantRepository : IRestaurantRepository
    {
        // GET: api/Restaurants
        [HttpGet]
        public IAsyncResult GetRestaurant()
        {
            throw new NotImplementedException();
            //return OK();
        }

        [HttpPut]
        public IAsyncResult PutRestaurant()
        {
            throw new NotImplementedException();
            //return OK();

        }
        [HttpPost]
        public IAsyncResult PostLocation()
        {
            throw new NotImplementedException();
            //return OK();
        }
        [HttpPost]
        public IAsyncResult PostRestaurants()
        {
            throw new NotImplementedException();
            //return OK();
        }
        [HttpPost]
        public IAsyncResult PostOrderDetails()
        {
            throw new NotImplementedException();
            //return OK();
        }
        [HttpDelete]
        public IAsyncResult DeleteRestaurant()
        {
            throw new NotImplementedException();
            //return OK();
        }
    }
}
