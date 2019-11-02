using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Application_Classes;
using ZomatoDemo.Repository.Authentication;
using ZomatoDemo.Web.Models;

namespace ZomatoDemo.Repository.Users
{
    public class UserRepository: IUserRepository
    {
        private readonly ZomatoDbContext _dbContext;
        private readonly UserManager<UserAC> _userManager;
        private readonly IJwtFactory _jwtFactory;

        public UserRepository(ZomatoDbContext dbContext, UserManager<UserAC> userManager, IJwtFactory jwtFactory)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
            this._jwtFactory = jwtFactory;
        }

        //get
        public async Task<IEnumerable<UserAC>> GetAllUsers()
        {
            var allusers = await _userManager.Users.ToListAsync();
            return allusers;
        }
        public async Task<UserAC> GetUserAsync(string Id)
        {
            var idUser = await _userManager.Users.Where(u => u.Id == Id).FirstAsync();
             return idUser;
        }

        //put
        public async Task<UserAC> EditUser(UserAC user)
        {
            var userEdit = await _dbContext.Users.Where(u => u.Id.Equals(user.Id)).FirstOrDefaultAsync();

            userEdit.FullName = user.FullName;
            userEdit.DateOfBirth = user.DateOfBirth;

            _dbContext.Users.Update(userEdit);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        //delete
        public async Task<bool> DeleteUser(string userId)
        {
            var userDel = await _dbContext.Users.Where(u => u.Id.Equals(userId)).FirstOrDefaultAsync();
            _dbContext.Users.Remove(userDel);
            await _dbContext.SaveChangesAsync();
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
