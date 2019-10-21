using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Application_Classes;
using ZomatoDemo.DomainModel.Models;
using ZomatoDemo.Repository.UnitOfWork;
using static ZomatoDemo.Repository.UnitOfWork.IUnitOfWorkRepository;

namespace ZomatoDemo.core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IUnitOfWorkRepository unitOfWork;

        public RestaurantController(IUnitOfWorkRepository unitOfWork)
        {
           
            this.unitOfWork = unitOfWork;
        }
        //Get:api/Restaurant

        [HttpGet]
        [Route("restaurant/{id}")]
        public async Task<ActionResult> GetRestaurantLocationAsync([FromRoute] int id)
        {
            return Ok(await unitOfWork.Restaurant.GetRestaurantLocation(id));
        }
        [HttpGet]
        [Route("allrestaurants")]
        public async Task<ActionResult> GetAllRestaurantAsync()
        {
            return Ok(await unitOfWork.Restaurant.GetRestaurants());
        }

        [HttpGet]
        [Route("restaurant/{userId}")]
        public async Task<ActionResult> GetUserRestaurantAsync([FromRoute] int userId)
        {
            return Ok(await unitOfWork.Restaurant.GetUserRestaurants(userId));
        }

        [HttpGet]
        [Route("restaurant/{restaurantId}")]
        public async Task<ActionResult> GetDishes([FromRoute] int restaurantId)
        {
            return Ok(await unitOfWork.Restaurant.GetDishes(restaurantId));
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

        [HttpPost]
        [Route("orderdetails")]
        public async Task<ActionResult> AddOrderDetails(OrderDetailsAC orderDetails)
        {
            await unitOfWork.Restaurant.AddOrderDetails(orderDetails);
            return Ok();
        }
        //put
        [HttpPut]
        [Route("restaurant/{id}")]
        public async Task<ActionResult> EditCart(int orderId, [FromBody] OrderDetailsAC orderDetailsac)
        {
            await unitOfWork.Restaurant.EditCart(orderId, orderDetailsac);
            return Ok();
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
