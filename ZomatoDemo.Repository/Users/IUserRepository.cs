using System;
using System.Collections.Generic;
using System.Text;
using ZomatoDemo.DomainModel.Models;

namespace ZomatoDemo.Repository.Users
{
    public interface IUserRepository
    {
        //get
        IEnumerable<User> GetAllUsers();
        User GetUser(long Id);

        //post
        object AddAllUsers(User user);

        //edit
        User EditUser(int userId);

        //delete
        bool DeleteUser(long userId);
    }
}
