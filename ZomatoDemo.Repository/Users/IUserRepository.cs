using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Models;

namespace ZomatoDemo.Repository.Users
{
    public interface IUserRepository
    {
        //get
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUser(int Id);

        //post
        Task<IEnumerable<User>> AddAllUsers(List<User> user);

        //edit
        Task<User> EditUser([FromBody] List<User> user);

        //delete
        Task<bool> DeleteUser(int userId);
    }
}
