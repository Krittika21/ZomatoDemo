using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZomatoDemo.Core.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult LogInView()
        {
            return View();
        }
    }
}
