using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Application_Classes;
using ZomatoDemo.DomainModel.Models;

namespace ZomatoDemo.Repository.Users
{
    public interface IUserRepository
    {
        //get
        Task<IEnumerable<UserAC>> GetAllUsers();
        Task<UserAC> GetUser(int Id);

        //post
        Task<IEnumerable<UserAC>> AddAllUsers(List<UserAC> user);

        //edit
        Task<UserAC> EditUser([FromBody] List<UserAC> user);

        //delete
        Task<bool> DeleteUser(int userId);
    }
}
