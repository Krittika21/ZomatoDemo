using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Application_Classes;

namespace ZomatoDemo.Core.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<UserAC> _signManager;
        private readonly UserManager<UserAC> _userManager;

        public AccountController(UserManager<UserAC> userManager, SignInManager<UserAC> signManager)
        {
            _userManager = userManager;
            _signManager = signManager;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogInView", "Account");
            }
            
        }

        // SIGNING UP NEW USER
        //[HttpGet]
        public IActionResult SignUpView()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromForm] RegisterAC registerAC)
        {
            if (ModelState.IsValid)
            {
                var user = new UserAC
                {
                    //Name = registerAC.UserName,
                    UserName = registerAC.Email
                };

                var result = await _userManager.CreateAsync(user, registerAC.Password);

                if (result.Succeeded)
                {
                    await _signManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }

        //LOGOUT
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Account");
        }

        //SIGNING IN OR LOGGIN IN
        [HttpGet]
        public IActionResult LogInView(string returnURL = "")
        {
            var model = new LoginAC
            {
                ReturnUrl = returnURL
            };
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginAC loginAC)
        {
            if (ModelState.IsValid)
            {
                var result = await _signManager.PasswordSignInAsync(loginAC.Email, loginAC.Password, loginAC.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(loginAC.ReturnUrl) && Url.IsLocalUrl(loginAC.ReturnUrl))
                    {
                        return Redirect(loginAC.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Account");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid Login Attempt");
            return View(loginAC);
        }
    }
}
