using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using ZomatoDemo.DomainModel.Models;
using ZomatoDemo.DomainModel.UserProfile;
using ZomatoDemo.Repository.Authentication;
using ZomatoDemo.Repository.Data_Repository;
using ZomatoDemo.Repository.UnitOfWork;
using ZomatoDemo.Repository.Users;

namespace ZomatoDemo.Repository.Test
{
    public class Bootstrap
    {
        public Mock<IDataRepository> dataRepository;
        public Mock<IJwtFactory> jwtMock;
        public ServiceProvider serviceProvider;
        public Mock<IMapper> _mapper;
        private Mock<UserManager<User>> userManager;

        public Bootstrap()
        {
            var services = new ServiceCollection();

            dataRepository = new Mock<IDataRepository>();
            userManager = new Mock<UserManager<User>>
                (
                    new Mock<IUserStore<User>>().Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<IPasswordHasher<User>>().Object,
                    new IUserValidator<User>[0],
                    new IPasswordValidator<User>[0],
                    new Mock<ILookupNormalizer>().Object,
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<IServiceProvider>().Object,
                    new Mock<ILogger<UserManager<User>>>().Object
               );
            jwtMock = new Mock<IJwtFactory>();
            _mapper = new Mock<IMapper>();

            //services.AddScoped<UserRepository, IUserRepository>();
            services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();
            services.AddScoped(obj => dataRepository.Object);
            services.AddScoped(obj => jwtMock.Object);
            services.AddScoped(obj => userManager.Object);
            //services.AddScoped(obj => _mapper.Object);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Profiles>();
            }).CreateMapper();

            services.AddSingleton(config);

            serviceProvider = services.BuildServiceProvider();
        }
    }
}
