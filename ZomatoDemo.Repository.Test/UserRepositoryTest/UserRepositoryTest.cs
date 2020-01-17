using Moq;
using System.Threading.Tasks;
using Xunit;
using ZomatoDemo.Repository.Data_Repository;
using ZomatoDemo.Repository.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using System;
using ZomatoDemo.DomainModel.Models;
using System.Collections.Generic;
using System.Linq;
using MockQueryable.Moq;

namespace ZomatoDemo.Repository.Test.UserRepositoryTest
{
    public class UserRepositoryTest : IClassFixture<Bootstrap>
    {
        private Mock<IDataRepository> _dataRepository;
        private IUnitOfWorkRepository _unitOfWorkRepository;

        public UserRepositoryTest(Bootstrap bootstrap)
        {
            _dataRepository = bootstrap.dataRepository;
            _unitOfWorkRepository = bootstrap.serviceProvider.GetService<IUnitOfWorkRepository>();
        }
        
        [Fact]
        public async Task EditUser_VerifyToUpdateUser()
        {
            List<User> users = new List<User>
            {
                new User
                {
                    Id = "dcbe1262-4be8-493a-8821-f4d52778d878",
                    FullName = "Nina Dobrev",
                    PhoneNumber = "9815686492"
                },
                new User
                {
                    Id = "3ae8bd2a-1a64-4120-b844-f2a0533718a4",
                    FullName = "Kat Graham",
                    PhoneNumber = "9815686492"
                }
            };

            User user = new User
            {
                Id = "dcbe1262-4be8-493a-8821-f4d52778d878",
                FullName = "Nina John Dobrev",
                PhoneNumber = "765764232"
            };

            _dataRepository.Setup(s => s.Where(It.IsAny<Expression<Func<User, bool>>>())).Returns(users.AsQueryable().BuildMock().Object);
            _dataRepository.Setup(s => s.Update(It.IsAny<User>()));
            //_dataRepository.Setup(s => s.SaveChangesAsync());

            await _unitOfWorkRepository.User.EditUser(user);

            _dataRepository.Verify(v => v.Update(It.IsAny<User>()));
            _dataRepository.Verify(v => v.SaveChangesAsync());
        }

        [Fact]
        public async Task DeleteUser_VerifyToDeleteAUser()
        {
            var userId = "dcbe1262-4be8-493a-8821-f4d52778d878";
            List<User> users = new List<User>
            {
                new User
                {
                    Id = "dcbe1262-4be8-493a-8821-f4d52778d878",
                    FullName = "Nina Dobrev",
                    PhoneNumber = "9815686492"
                },
                new User
                {
                    Id = "3ae8bd2a-1a64-4120-b844-f2a0533718a4",
                    FullName = "Kat Graham",
                    PhoneNumber = "9815686492"
                }
            };
            _dataRepository.Setup(s => s.Where(It.IsAny<Expression<Func<User, bool>>>())).Returns(users.AsQueryable().BuildMock().Object);
            //_dataRepository.Setup(s => s.Remove(It.IsAny<IEnumerable<User>>()));
           // _dataRepository.Setup(s => s.SaveChangesAsync());
            
            await _unitOfWorkRepository.User.DeleteUser(userId);

            _dataRepository.Verify(v => v.Remove(It.IsAny<User>()), times: Times.Once);
            _dataRepository.Verify(v => v.SaveChangesAsync());
        }
    }
}
