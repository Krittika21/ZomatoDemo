using System;
using System.Collections.Generic;
using System.Text;
using ZomatoDemo.DomainModel.Models;

namespace ZomatoDemo.Repository.Users
{
    public interface IUserRepository
    {
            object AddUser(User user);
            IEnumerable<User> GetUsers();
            bool DeleteUser(long userId);
            User GetUser(long Id);
            User EditUser(int userId);
    }
}
