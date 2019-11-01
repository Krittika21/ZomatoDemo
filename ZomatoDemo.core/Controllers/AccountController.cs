using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Application_Classes;
using ZomatoDemo.DomainModel.Utility;
using ZomatoDemo.Repository.Authentication;
using ZomatoDemo.Repository.Helpers;
using ZomatoDemo.Repository.UnitOfWork;

namespace ZomatoDemo.Core.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly SignInManager<UserAC> _signManager;
        private readonly UserManager<UserAC> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IUnitOfWorkRepository unitOfWork;

        public AccountController(UserManager<UserAC> userManager, SignInManager<UserAC> signManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions, IUnitOfWorkRepository unitOfWork)
        {
            _userManager = userManager;
            _signManager = signManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
            this.unitOfWork = unitOfWork;
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
        [HttpGet]
        [Route("signUp")]
        public IActionResult SignUpView()
        {
            return View();
        }

        [HttpPost]
        [Route("signUp")]
        public async Task<IActionResult> SignUp([FromBody] RegisterAC registerAC)
        {
            if (ModelState.IsValid)
            {
                var user = new UserAC
                {
                    Email = registerAC.Email,
                    UserName = registerAC.UserName
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
                    return new OkObjectResult("Invalid Credentials");
                }
            }
            return new OkObjectResult("OKay");

            //return View();
        }

        // POST api/Account/login
        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]LoginAC loginAC)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = await unitOfWork.User.GetClaimsIdentity(loginAC.Email, loginAC.Password);

            if (identity == null)
            {
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid email or password.", ModelState));
            }

            var jwt = await Tokens.GenerateJwt(identity, _jwtFactory, loginAC.Email, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return new OkObjectResult(jwt);
        }

        //LOGOUT
        [HttpPost]
        [Route("logOut")]
        public async Task<IActionResult> LogOut()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Account");
        }

        ////SIGNING IN OR LOGGIN IN
        //[HttpGet]
        //[Route("logIn")]
        //public IActionResult LogInView(string returnURL = "")
        //{
        //    var model = new LoginAC
        //    {
        //        ReturnUrl = returnURL
        //    };
        //    return View();
        //}

        //[HttpPost]
        //[Route("logIn")]
        //public async Task<IActionResult> LogIn(LoginAC loginAC)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _signManager.PasswordSignInAsync(loginAC.Email, loginAC.Password, loginAC.RememberMe, false);
        //        if (result.Succeeded)
        //        {
        //            if (!string.IsNullOrEmpty(loginAC.ReturnUrl) && Url.IsLocalUrl(loginAC.ReturnUrl))
        //            {
        //                return Redirect(loginAC.ReturnUrl);
        //            }
        //            else
        //            {
        //                return RedirectToAction("Index", "Account");
        //            }
        //        }
        //    }
        //    ModelState.AddModelError("", "Invalid Login Attempt");
        //    return View(loginAC);
        //}
    }
}
