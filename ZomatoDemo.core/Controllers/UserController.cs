using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Models;
using static ZomatoDemo.Repository.UnitOfWork.IUnitOfWorkRepository;


namespace ZomatoDemo.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
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
        public async Task<ActionResult> GetUser(int Id)
        {
            return Ok(await unitOfWork.User.GetUser(Id));
        }

        //post
        [HttpPost]
        [Route("allusers")]
        public async Task<ActionResult> PostUsers(List<User> user)
        {
            return Ok(await unitOfWork.User.AddAllUsers(user));
        }

        //put
        [HttpPut]
        [Route("user/{id}")]
        public async Task<ActionResult> UpdateUser([FromBody] List<User> user)
        {
            return Ok(await unitOfWork.User.EditUser(user));
        }

        //delete
        [HttpDelete]
        [Route("user/{id}")]
        public async Task<ActionResult> RemoveUser(int userId)
        {
            return Ok(await unitOfWork.User.DeleteUser(userId));
        }
    }
}
