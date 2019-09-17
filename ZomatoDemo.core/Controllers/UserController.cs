using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
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
            return Ok(unitOfWork.User.GetAllUsers());
        }

        [HttpGet]
        [Route("user/{id}")]
        public async Task<ActionResult> GetUser(long Id)
        {
            return Ok(unitOfWork.User.GetUser(Id));
        }

        //post
        [HttpPost]
        [Route("allusers")]
        public async Task<ActionResult> PostUsers(User user)
        {
            return Ok(unitOfWork.User.AddAllUsers(user));
        }

        //put
        [HttpPut]
        [Route("user/{id}")]
        public async Task<ActionResult> UpdateUser(int userId)
        {
            return Ok(unitOfWork.User.EditUser(userId));
        }

        //delete
        [HttpDelete]
        [Route("user/{id}")]
        public async Task<ActionResult> RemoveUser(long userId)
        {
            return Ok(unitOfWork.User.DeleteUser(userId));
        }
    }
}
