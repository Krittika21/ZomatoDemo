using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Application_Classes;
using ZomatoDemo.DomainModel.Models;
using ZomatoDemo.Web.Models;

namespace ZomatoDemo.Repository.Users
{
    public class UserRepository: IUserRepository
    {
        private readonly ZomatoDbContext _dbContext;
        private readonly UserManager<UserAC> _userManager;

        public UserRepository(ZomatoDbContext dbContext, UserManager<UserAC> userManager)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
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
    }
}
