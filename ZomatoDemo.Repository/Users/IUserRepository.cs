using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Application_Classes;

namespace ZomatoDemo.Repository.Users
{
    public interface IUserRepository
    {
        //get
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserAsync(string Id);

        //edit
        Task<User> EditUser(User user);

        //delete
        Task<bool> DeleteUser(string userId);

        //login
        Task<ClaimsIdentity> GetClaimsIdentity(string email, string password);

    }
}
