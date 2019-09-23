using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Application_Classes;
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
        [Route("restaurant/{id}")]
        public async Task<ActionResult> GetRestaurantLocationAsync(int restaurantId)
        {
            return Ok(await unitOfWork.Restaurant.GetRestaurantLocation(restaurantId));
        }
        [HttpGet]
        [Route("allrestaurants")]
        public async Task<ActionResult> GetAllRestaurantAsync()
        {
            return Ok(await unitOfWork.Restaurant.GetRestaurants());
        }

        [HttpGet]
        [Route("restaurant/{userId}")]
        public async Task<ActionResult> GetUserRestaurantAsync(int userId)
        {
            return Ok(await unitOfWork.Restaurant.GetUserRestaurants(userId));
        }

        //post
        [HttpPost]
        [Route("location")]
        public async Task<ActionResult> PostLocationAsync(List<LocationAC> locationAC)
        {
            return Ok(await unitOfWork.Restaurant.AddLocation(locationAC));
        }

        [HttpPost]
        [Route("allrestaurants")]
        public async Task<ActionResult> PostAllRestaurantAsync(List<RestaurantAC> restaurants)
        {
            return Ok(await unitOfWork.Restaurant.AddAllRestaurants(restaurants));
        }

        //put
        [HttpPut]
        [Route("restaurant/{id}")]
        public async Task<ActionResult> UpdateRestaurantAsync([FromBody] List<Restaurant> restaurants)
        {
            return Ok(await unitOfWork.Restaurant.EditRestaurant(restaurants));
        }

        //delete
        [HttpDelete]
        [Route("restaurant/{id}")]
        public async Task<ActionResult> RemoveRestaurantAsync(int restaurantId)
        {
            return Ok(await unitOfWork.Restaurant.DeleteRestaurant(restaurantId));
        }
    }
}
