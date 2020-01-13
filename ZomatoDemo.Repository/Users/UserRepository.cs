using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Application_Classes;
using ZomatoDemo.DomainModel.Models;
using ZomatoDemo.Repository.Authentication;
using ZomatoDemo.Repository.Data_Repository;
using ZomatoDemo.Web.Models;

namespace ZomatoDemo.Repository.Users
{
    public class UserRepository: IUserRepository
    {
        //private readonly ZomatoDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly IDataRepository _dataRepository;

        public UserRepository(UserManager<User> userManager, IJwtFactory jwtFactory, IDataRepository dataRepository)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _dataRepository = dataRepository;
        }

        //get
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var allusers = await _userManager.Users.ToListAsync();
            return allusers;
        }
        public async Task<User> GetUserAsync(string Id)
        {
            var idUser = await _userManager.Users.Where(u => u.Id == Id).FirstAsync();
             return idUser;
        }

        //put
        public async Task<User> EditUser(User user)
        {
            var userEdit = await _dataRepository.Where<User>(u => u.Id.Equals(user.Id)).FirstOrDefaultAsync();

            userEdit.FullName = user.FullName;
            userEdit.PhoneNumber = user.PhoneNumber;

            _dataRepository.Update<User>(userEdit);
            await _dataRepository.SaveChangesAsync();
            return user;
        }

        //delete
        public async Task<bool> DeleteUser(string userId)
        {
            var userDel = await _dataRepository.Where<User>(u => u.Id.Equals(userId)).FirstOrDefaultAsync();
            _dataRepository.Remove<User>(userDel);
            await _dataRepository.SaveChangesAsync();
            return true;
        }

        //login
        public async Task<ClaimsIdentity> GetClaimsIdentity(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByEmailAsync(email);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(email, userToVerify.Id));
            } 

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }

    }
}
