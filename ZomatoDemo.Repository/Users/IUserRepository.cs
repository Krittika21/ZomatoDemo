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
        User GetUser(int Id);

        //post
        object AddAllUsers(List<User> user);

        //edit
        User EditUser(int userId);

        //delete
        bool DeleteUser(int userId);
    }
}
