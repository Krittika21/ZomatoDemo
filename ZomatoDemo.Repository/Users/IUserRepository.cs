using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
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
        //IEnumerable<User> AddAllUsers(List<User> user);
        object AddAllUsers(List<User> user);

        //edit
        User EditUser([FromBody] List<User> user);

        //delete
        bool DeleteUser(int userId);
    }
}
