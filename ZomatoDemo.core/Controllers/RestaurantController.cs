using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Models;
using static ZomatoDemo.Repository.UnitOfWork.IUnitOfWorkRepository;

namespace ZomatoDemo.core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public RestaurantController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        //Get:api/Restaurant

        [HttpGet]
        public async Task<ActionResult> GetAllRestaurantAsync()
        {
            return Ok(unitOfWork.Restaurant.GetRestaurants());
        }

        [HttpGet]
        public async Task<ActionResult> GetUserRestaurant(long userId)
        {
            return Ok(unitOfWork.Restaurant.GetUserRestaurants(userId));
        }

        [HttpPost]
        public async Task<ActionResult> PostLocation(Location location)
        {
            return Ok(unitOfWork.Restaurant.AddLocation(location));
        }

        //post
        [HttpPost]
        public async Task<ActionResult> PostRestaurant(Restaurant restaurants)
        {
            return Ok(unitOfWork.Restaurant.AddRestaurant(restaurants));
        }

        [HttpPost]
        public async Task<ActionResult> PostRestaurantToUser(long userId, long restaurantId)
        {
            return Ok(unitOfWork.Restaurant.AddRestaurantToUser(userId, restaurantId));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateRestaurant(long restaurantId)
        {
            return Ok(unitOfWork.Restaurant.EditRestaurant(restaurantId));
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveRestaurant(long restaurantId)
        {
            return Ok(unitOfWork.Restaurant.DeleteRestaurant(restaurantId));
        }
    }
}
