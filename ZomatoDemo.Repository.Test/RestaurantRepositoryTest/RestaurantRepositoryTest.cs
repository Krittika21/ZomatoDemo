using Moq;
using Xunit;
using ZomatoDemo.Repository.Data_Repository;
using Microsoft.Extensions.DependencyInjection;
using ZomatoDemo.Repository.UnitOfWork;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Models;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using ZomatoDemo.Repository.Test;
using MockQueryable.Moq;
using ZomatoDemo.DomainModel.Application_Classes;

namespace ZomatoDemo.Repository.Test.RestaurantRepositoryTest
{
    public class RestaurantRepositoryTest : IClassFixture<Bootstrap>
    {
        private Mock<IDataRepository> _dataRepository;
        private IUnitOfWorkRepository _unitOfWorkRepository;

        public RestaurantRepositoryTest(Bootstrap bootstrap)
        {
            _dataRepository = bootstrap.dataRepository;
            _unitOfWorkRepository = bootstrap.serviceProvider.GetService<IUnitOfWorkRepository>();
        }

        [Fact]
        public async Task AddLocation_VarifyWhetherLocationIsGettingAddedOrNot()
        {
            List<Location> locations = new List<Location>
            {
                new Location
                {
                    City = new City
                    {
                            ID = 1,
                            CityName = "Kolkata",
                    },
                    Country = new Country
                    {
                        ID = 1,
                        CountryName = "India"
                    }
                }
            };
            List<LocationAC> locationACs = new List<LocationAC>
            {
                new LocationAC
                {
                   ID = 1,
                   CityName = "Kolkata"
                }
            };
            _dataRepository.Setup(l => l.Where(It.IsAny<Expression<Func<Location, bool>>>())).Returns(locations.AsQueryable().BuildMock().Object);
            await _unitOfWorkRepository.Restaurant.AddLocation(locationACs);
            _dataRepository.Verify(d => d.AddRangeAsync(It.IsAny<City>()), Times.Once);
            _dataRepository.Verify(d => d.SaveChangesAsync());
        }

        [Fact]
        public async Task AddRestaurants_VerifyAdditionOfRestaurantsWithDetails()
        {
            AllDetails allDetails = new AllDetails
            {
                LocationID = 1,
                RestaurantID = 2,
                Locality = "Manjalpur",
                RestaurantName = "Domino's",
                ContactNumber = "68886888",
                CuisineType = "Italian",
                AverageCost = "500",
                OpeningHours = "10AM - 11PM",
                MoreInfo = "Pet friendly",
                City = new AllCity
                {
                    ID = 1,
                    CityName = "Kolkata",
                },
                Country = new AllCountry
                {
                    ID = 1,
                    CountryName = "India"
                },
                AllReviews = new List<ReviewsAC>
                {
                   new ReviewsAC
                   {
                       userID = "",
                       UserName = "Mr. X",
                       LikesCount = 2,
                       ReviewId = 1,
                       ReviewTexts = "Hey",
                       commentACs = new List<CommentAC>
                       {
                           new CommentAC
                           {
                               ID = 1,
                               CommentMessage = "Good",
                               UserID = "",
                               FullName = "Olivia Bose",
                               ReviewID = 1
                           }
                       }
                   }
                }
            };
            _dataRepository.Setup(d => d.Where(It.IsAny<Expression<Func<Restaurant, bool>>>())).Returns(AllDetails.AsQueryable().BuildMock().Object);
            await _unitOfWorkRepository.Restaurant.NewDish(1, allDetails);
            _dataRepository.Verify(d => d.AddAsync(It.IsAny<Dishes>()), Times.Once);
            _dataRepository.Verify(d => d.SaveChangesAsync());
        }

        [Fact]
        public async Task NewDish_VerifyThatNewDishIsAddedToARestaurant()
        {
            List<Restaurant> newResturant = new List<Restaurant>
            {
                new Restaurant
                {
                    Dishes = new List<Dishes>
                    {
                        new Dishes
                        {
                            ID = 1,
                            DishesName = "burger",
                            Costs = 200
                        },
                        new Dishes
                        {
                            ID = 2,
                            DishesName = "pizza",
                            Costs = 400
                        }
                    }
                }
            };
            AllDishes newDishes = new AllDishes
            {
                ID = 3,
                DishesName = "taco",
                Costs = 150
            };

            _dataRepository.Setup(d => d.Where(It.IsAny<Expression<Func<Restaurant, bool>>>())).Returns(newResturant.AsQueryable().BuildMock().Object);
            await _unitOfWorkRepository.Restaurant.NewDish(1, newDishes);
            _dataRepository.Verify(d => d.AddAsync(It.IsAny<Dishes>()), Times.Once);
            _dataRepository.Verify(d => d.SaveChangesAsync());
        }


    }
}

