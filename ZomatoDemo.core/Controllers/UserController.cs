using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Application_Classes;
using ZomatoDemo.DomainModel.Models;
using ZomatoDemo.Repository.UnitOfWork;
using static ZomatoDemo.Repository.UnitOfWork.IUnitOfWorkRepository;


namespace ZomatoDemo.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWorkRepository unitOfWork;
        public UserController(IUnitOfWorkRepository unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        //Get:api/Restaurant
       
        [HttpGet]
        [Route("allusers")]
        public async Task<ActionResult> GetAllUsers()
        {
            return Ok(await unitOfWork.User.GetAllUsers());
        }

        [HttpGet]
        [Route("user/{id}")]
        public async Task<ActionResult> GetUser(string Id)
        {
            return Ok(await unitOfWork.User.GetUserAsync(Id));
        }

        //put
        [HttpPut]
        [Route("user/{id}")]
        public async Task<ActionResult> UpdateUser(UserAC user)
        {
            return Ok(await unitOfWork.User.EditUser(user));
        }

        //delete
        [HttpDelete]
        [Route("user/{id}")]
        public async Task<ActionResult> RemoveUser(string userId)
        {
            return Ok(await unitOfWork.User.DeleteUser(userId));
        }
    }
}
