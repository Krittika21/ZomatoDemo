using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Application_Classes;
using ZomatoDemo.Repository.UnitOfWork;

namespace ZomatoDemo.core.Controllers
{//9123361921
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
        public async Task<ActionResult> GetRestaurantDetailsAsync([FromRoute] int id)
        {
            return Ok(await unitOfWork.Restaurant.GetRestaurantDetails(id));
        }

        [HttpGet]
        [Route("allrestaurants")]
        public async Task<ActionResult> GetAllRestaurantAsync()
        {
            return Ok(await unitOfWork.Restaurant.GetAllRestaurants());
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

        //[HttpGet]
        //[Route("restaurant/comment/{restaurantId}")]
        //public async Task<ActionResult> Comments([FromRoute] int reviewId)
        //{
        //    var x = await unitOfWork.Restaurant.Comments(reviewId);
        //    return Ok(x);
        //}


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
            var o = await unitOfWork.Restaurant.AddOrderDetails(orderDetails);
            return Ok(o);
        }

        [HttpPost]
        [Route("newdish/{restaurantId}")]
        public async Task<ActionResult> NewḌish([FromRoute] int restaurantId, AllDishes dishes)
        {
            var d = await unitOfWork.Restaurant.NewDish(restaurantId, dishes);
            return Ok(d);
        }

        [HttpPost]
        [Route("reviews/{restaurantId}")]
        public async Task<ActionResult> AddReviews([FromRoute]int restaurantId, [FromBody]ReviewsAC reviews)
        {
            var r = await unitOfWork.Restaurant.AddReviews(restaurantId, reviews);
            return Ok(r);
        }

        [HttpPost]
        [Route("reviewsLikes/{restaurantId}")]
        public async Task<IActionResult> Likes(ReviewsAC review)
        {
            var x = await unitOfWork.Restaurant.Likes(review.ReviewId, review.userID);
            return Ok(x);
        }

        [HttpPost]
        [Route("comment/{restaurantId}")]
        public async Task<IActionResult> CommentSection([FromRoute] int restaurantId, CommentAC commentac)
        {
            var c = await unitOfWork.Restaurant.CommentSection(restaurantId, commentac);
            return Ok(c);
        }

        //put
        //[HttpPut]
        //[Route("restaurant/{id}")]
        //public async Task<ActionResult> EditCart(int orderId, [FromBody] OrderDetailsAC orderDetailsac)
        //{
        //    await unitOfWork.Restaurant.EditCart(orderId, orderDetailsac);
        //    return Ok();
        //}

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
