using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Models;
using ZomatoDemo.Web.Models;

namespace ZomatoDemo.Repository.Users
{
    public class UserRepository: IUserRepository
    {
        private readonly ZomatoDbContext _dbContext;

        public UserRepository(ZomatoDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        //get
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            await _dbContext.SaveChangesAsync();
            throw new NotImplementedException();
        }
        public async Task<User> GetUser(int Id)
        {
            await _dbContext.SaveChangesAsync();
            throw new NotImplementedException();
        }

        //post
        public async Task<IEnumerable<User>> AddAllUsers(List<User> user)
        {
            await _dbContext.SaveChangesAsync();
            throw new NotImplementedException();
        }

        //put
        public async Task<User> EditUser([FromBody] List<User> user)
        {
            await _dbContext.SaveChangesAsync();
            throw new NotImplementedException();
        }

        //delete
        public async Task<bool> DeleteUser(int userId)
        {
            await _dbContext.SaveChangesAsync();
            throw new NotImplementedException();
        }
    }
}
