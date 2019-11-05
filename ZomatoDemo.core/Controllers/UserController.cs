using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<UserAC> _userManager;
        public UserController(IUnitOfWorkRepository unitOfWork, UserManager<UserAC> userManager)
        {
            this.unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        //Get:api/Restaurant
       
        [HttpGet]
        [Route("allusers")]
        public async Task<ActionResult> GetAllUsers()
        {
            return Ok(await unitOfWork.User.GetAllUsers());
        }

        [HttpGet]
        [Route("currentUser")]
        public async Task<ActionResult> GetCurrentUser()
        {
            var loggedInUserName= User.Identity.Name;
            var user = await _userManager.FindByNameAsync(loggedInUserName);
            var userRoles = await _userManager.GetRolesAsync(user);
            CurrentUser userAC = new CurrentUser();
            userAC.UserId = user.Id;
            userAC.UserName = user.Email;
            userAC.email = user.Email;
            userAC.Roles = userRoles;
            return Ok(userAC);
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
