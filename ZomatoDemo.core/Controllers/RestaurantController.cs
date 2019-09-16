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
        public async Task<ActionResult> GetRestaurantLocationAsync(long restaurantId)
        {
            return Ok(unitOfWork.Restaurant.GetRestaurantLocation(restaurantId));
        }
        [HttpGet]
        public async Task<ActionResult> GetAllRestaurantAsync()
        {
            return Ok(unitOfWork.Restaurant.GetRestaurants());
        }

        [HttpGet]
        public async Task<ActionResult> GetUserRestaurantAsync(long userId, long restaurantId)
        {
            return Ok(unitOfWork.Restaurant.GetUserRestaurants(userId, restaurantId));
        }

        //post
        
        [HttpPost]
        public async Task<ActionResult> PostLocationAsync(Location location)
        {
            return Ok(unitOfWork.Restaurant.AddLocation(location));
        }

        [HttpPost]
        public async Task<ActionResult> PostAllRestaurantAsync(Restaurant restaurants)
        {
            return Ok(unitOfWork.Restaurant.AddAllRestaurants(restaurants));
        }

        //put
        [HttpPut]
        public async Task<ActionResult> UpdateRestaurantAsync(long restaurantId)
        {
            return Ok(unitOfWork.Restaurant.EditRestaurant(restaurantId));
        }

        //delete
        [HttpDelete]
        public async Task<ActionResult> RemoveRestaurantAsync(long restaurantId)
        {
            return Ok(unitOfWork.Restaurant.DeleteRestaurant(restaurantId));
        }
    }
}
