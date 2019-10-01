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
        Task<UserAC> GetUserAsync(string Id);

        //edit
        Task<UserAC> EditUser(UserAC user);

        //delete
        Task<bool> DeleteUser(string userId);
    }
}
