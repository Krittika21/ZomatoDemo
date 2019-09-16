using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZomatoDemo.Repository.Users
{
    public class UserRepository: IUserRepository
    {
        [HttpGet]
        public IAsyncResult GetUser()
        {
            throw new NotImplementedException();
            //return OK();
        }
        [HttpPost]
        public IAsyncResult PostUser()
        {
            throw new NotImplementedException();
            //return OK();
        }
        [HttpPut]
        public IAsyncResult PutUser()
        {
            throw new NotImplementedException();
            //return OK();
        }
        [HttpDelete]
        public IAsyncResult DeleteUser()
        {
            throw new NotImplementedException();
            //return OK();
        }
    }
}
