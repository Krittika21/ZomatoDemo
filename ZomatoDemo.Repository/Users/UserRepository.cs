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
        private readonly DbContext dbContext;

        public UserRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //get
        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }
        public User GetUser(long Id)
        {
            throw new NotImplementedException();
        }

        //post
        public object AddAllUsers(User user)
        {
            throw new NotImplementedException();
        }

        //put
        public User EditUser(int userId)
        {
            throw new NotImplementedException();
            //return OK();
        }

        //delete
        public bool DeleteUser(long userId)
        {
            throw new NotImplementedException();
        }
    }
}
