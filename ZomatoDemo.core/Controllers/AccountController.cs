using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
using ZomatoDemo.Web.Models;

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
        private ZomatoDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<UserAC> userManager, SignInManager<UserAC> signManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions, IUnitOfWorkRepository unitOfWork, ZomatoDbContext dbContext, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signManager = signManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
            this.unitOfWork = unitOfWork;
            _dbContext = dbContext;
            _roleManager = roleManager;
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
                    UserName = registerAC.UserName,
                    FullName = registerAC.FullName,
                    PhoneNumber = registerAC.PhoneNumber
                };

                var result = await _userManager.CreateAsync(user, registerAC.Password);
                var useremail = await _userManager.FindByEmailAsync(user.Email);
                var roles = await _userManager.AddToRoleAsync(useremail , "user");
                if (result.Succeeded)
                {
                    await _signManager.SignInAsync(user, false);
                    return Ok(true);

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

        [HttpPost]
        [Route("signUpAdmin")]
        public async Task<IActionResult> SignUpAdmin([FromBody] RegisterAC registerAC)
        {
            if (ModelState.IsValid)
            {
                var user = new UserAC
                {
                    Email = registerAC.Email,
                    UserName = registerAC.UserName
                };

                var result = await _userManager.CreateAsync(user, registerAC.Password);
                var usr = await _userManager.FindByEmailAsync(user.Email);
                //await _userManager.AddClaimAsync(usr, new Claim(ClaimTypes.Role, "user"));
                var roles = await _userManager.AddToRoleAsync(usr, "admin");
                if (result.Succeeded)
                {
                    await _signManager.SignInAsync(user, false);
                    return Ok(true);

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

        [HttpPost]
        [Route("roles")]
        public async Task<IActionResult> Roles([FromBody] IdentityRole role)
        {
            var x = await _roleManager.RoleExistsAsync(role.Name);
            if (!x)
            {
                var roles = new IdentityRole();
                roles.Name = role.Name;
                await _roleManager.CreateAsync(roles);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
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
            var userEmail = await _userManager.FindByEmailAsync(loginAC.Email);
            var y = await _userManager.GetRolesAsync(userEmail);
            if (identity == null)
            {
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid email or password.", ModelState));
            }

            var jwt = await Tokens.GenerateJwt(userEmail, y, identity, _jwtFactory, loginAC.Email, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
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
    }
}
