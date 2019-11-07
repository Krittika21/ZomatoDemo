using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Application_Classes;
using ZomatoDemo.Repository.UnitOfWork;

namespace ZomatoDemo.core.Controllers
{
    // [Authorize(Policy = "Users")]
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
        //[Authorize(Policy = "ApiUser")]
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
        [Route("restaurant/dishes/{restaurantId}")]
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
        public async Task<ActionResult> PostAllRestaurantAsync(AllDetails restaurants)
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

        [HttpPost]
        [Route("newdish/{restaurantId}")]
        public async Task<ActionResult> NewḌish([FromRoute] int restaurantId, AllDishes dishes)
        {
            await unitOfWork.Restaurant.NewDish(restaurantId, dishes);
            return Ok();
        }

        [HttpPost]
        [Route("reviews/{restaurantId}")]
        public async Task<ActionResult> AddReviews([FromRoute]int restaurantId, [FromBody]ReviewsAC reviews)
        {
            await unitOfWork.Restaurant.AddReviews(restaurantId, reviews);
            return Ok();
        }

        //public Task<ActionResult> Likes(int userId)
        //{
        //   // ReviewsAC reviewLikes = await _dbContext
        //    return Ok();
        //}

        //put
        [HttpPut]
        [Route("restaurant/{id}")]
        public async Task<ActionResult> EditCart(int orderId, [FromBody] OrderDetailsAC orderDetailsac)
        {
            await unitOfWork.Restaurant.EditCart(orderId, orderDetailsac);
            return Ok();
        }

        [HttpPut]
        [Route("editrestaurant/{id}")]
        public async Task<ActionResult> EditRestaurant([FromRoute] int id, [FromBody] AllDetails details)
        {
            await unitOfWork.Restaurant.EditRestaurant(id, details);
            return Ok();
        }

        //delete
        [HttpDelete]
        [Route("restaurant/{id}")]
        public async Task<ActionResult> RemoveRestaurantAsync([FromRoute] int id)
        {
            await unitOfWork.Restaurant.DeleteRestaurant(id);
            return Ok();
        }

        [HttpDelete]
        [Route("menu/dishes/{id}")]
        public async Task<ActionResult> RemoveDishes([FromRoute] int id)
        {
            await unitOfWork.Restaurant.DeleteDishes(id);
            return Ok();
        }
    }
}
