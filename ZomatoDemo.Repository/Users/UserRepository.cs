using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Models;

namespace ZomatoDemo.Repository.Users
{
    public class UserRepository: IUserRepository
    {
        private readonly DbContext _dbContext;

        public UserRepository(DbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        //get
        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }
        public User GetUser(int Id)
        {
            throw new NotImplementedException();
        }

        //post
        public object AddAllUsers(List<User> user)
        {
            throw new NotImplementedException();
        }

        //put
        public User EditUser([FromBody] List<User> user)
        {
            throw new NotImplementedException();
            //return OK();
        }

        //delete
        public bool DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
