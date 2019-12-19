using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using ZomatoDemo.Core.Hubs;
using ZomatoDemo.DomainModel.Application_Classes;
using ZomatoDemo.DomainModel.Models;
using ZomatoDemo.DomainModel.Utility;
using ZomatoDemo.Repository.Authentication;
using ZomatoDemo.Repository.Helpers;
using ZomatoDemo.Repository.UnitOfWork;
using ZomatoDemo.Web.Models;

namespace ZomatoDemo
{
    public class Startup
    {
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver
                = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            });

          

                services.AddDbContext<ZomatoDbContext>(options =>

                options.UseSqlServer(Configuration.GetConnectionString("ZomatoDbContext"), 
                b => b.MigrationsAssembly("ZomatoDemo.DomainModel")));
           
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
                .AddEntityFrameworkStores<ZomatoDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Index";
                options.AccessDeniedPath = "/Account/Index";
            });

            // Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = false,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;

            });

            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
            });

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<City, AllCity>().ReverseMap();
                cfg.CreateMap<Country, AllCountry>().ReverseMap();
                //AllDetails
                cfg.CreateMap<Restaurant, AllDetails>().ForMember(destination => destination.RestaurantID,
               opts => opts.MapFrom(source => source.ID)).ReverseMap();

                cfg.CreateMap<Location, AllDetails>().ForMember(destination => destination.LocationID,
                    acts => acts.MapFrom(source => source.ID)).ReverseMap();

                cfg.CreateMap<City, AllDetails>().ReverseMap();
                cfg.CreateMap<Country, AllDetails>().ReverseMap();
                cfg.CreateMap<Review, AllDetails>().ReverseMap();

                cfg.CreateMap<Dishes, AllDishes>().ReverseMap();
                cfg.CreateMap<Restaurant, AllRestaurants>();
                //CommentAC
                cfg.CreateMap<Comment, CommentAC>().ReverseMap();
                cfg.CreateMap<User, CommentAC>();

                cfg.CreateMap<User, CurrentUser>();
                cfg.CreateMap<Location,LocationAC>();
                cfg.CreateMap<User, LoginAC>();
                cfg.CreateMap<OrderDetails, OrderDetailsAC>().ReverseMap();
                //RestaurantAC
                cfg.CreateMap<User, RegisterAC>();
                cfg.CreateMap<Restaurant, RestaurantAC>();
                cfg.CreateMap<Dishes, RestaurantAC>();
                cfg.CreateMap<Location, RestaurantAC>();

                cfg.CreateMap<OrderDetailsAC, User>().ReverseMap();
                cfg.CreateMap<OrderDetailsAC, Restaurant>().ReverseMap();
                cfg.CreateMap<OrderDetailsAC, DishesOrdered>().ReverseMap();

                cfg.CreateMap<Review, ReviewsAC>().ForMember(destination => destination.ReviewId,
               opts => opts.MapFrom(source => source.ID)).ReverseMap();

                cfg.CreateMap<ReviewsAC, User>().ForMember(destination => destination.UserName,
               opts => opts.MapFrom(source => source.UserName)).ForMember(destination => destination.Id,
               opts => opts.MapFrom(source => source.userID));
            });

            IMapper mapper = config.CreateMapper();

            services.AddSingleton(mapper);
            services.AddCors();
            services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();
            services.AddScoped<IJwtFactory, JwtFactory>();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseCors(Options =>
            Options
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowCredentials()
            .AllowAnyHeader());

            app.UseSignalR(routes =>
            {
                routes.MapHub<NotifyHub>("/notify");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Index}");
            });
        }
    }
}
