using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public UserRepository(ZomatoDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        //get
        public async Task<IEnumerable<UserAC>> GetAllUsers()
        {
            await _dbContext.SaveChangesAsync();
            throw new NotImplementedException();
        }
        public async Task<UserAC> GetUser(int Id)
        {
            await _dbContext.SaveChangesAsync();
            throw new NotImplementedException();
        }

        //post
        public async Task<IEnumerable<UserAC>> AddAllUsers(List<UserAC> user)
        {
            await _dbContext.SaveChangesAsync();
            throw new NotImplementedException();
        }

        //put
        public async Task<UserAC> EditUser([FromBody] List<UserAC> user)
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
