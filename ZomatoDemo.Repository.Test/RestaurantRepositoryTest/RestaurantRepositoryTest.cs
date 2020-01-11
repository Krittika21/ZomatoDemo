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
        public async Task GetRestaurantDetails_VerifyToGetTheLocationOfARestaurant()
        {
            List<Restaurant> restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    ID = 1
                }
            };
            List<Country> countries = new List<Country>
            {
                new Country
                {

                }
            };
            List<City> cities = new List<City>
            {
                new City
                {

                }
            };
            List<Review> reviews = new List<Review>
            {
                new Review
                {

                }
            };
            List<Comment> comments = new List<Comment>
            {
                new Comment
                {

                }
            };

            List<AllDetails> allDetails = new List<AllDetails>
            {
                new AllDetails
                {
                    RestaurantID = 1,
                    LocationID = 1
                },
                new AllDetails
                {
                    RestaurantID = 1,
                    LocationID = 2
                },
                new AllDetails
                {
                    RestaurantID = 2,
                    LocationID = 1
                }
            };

            _dataRepository.SetupSequence(s => s.Where(It.IsAny<Expression<Func<Restaurant, bool>>>())).Returns(restaurants.AsQueryable().BuildMock().Object).Returns(restaurants.AsQueryable().BuildMock().Object);
            _dataRepository.Setup(s => s.Where(It.IsAny<Expression<Func<Country, bool>>>())).Returns(countries.AsQueryable().BuildMock().Object);
            _dataRepository.Setup(s => s.Where(It.IsAny<Expression<Func<City, bool>>>())).Returns(cities.AsQueryable().BuildMock().Object);
            _dataRepository.Setup(s => s.Where(It.IsAny<Expression<Func<Review, bool>>>())).Returns(reviews.AsQueryable().BuildMock().Object);
            _dataRepository.Setup(s => s.GetAll<Comment>()).Returns(comments.AsQueryable().BuildMock().Object);

            var actualResult = await _unitOfWorkRepository.Restaurant.GetRestaurantDetails(1);

            var expectedResult = allDetails.Where(s => s.RestaurantID == 1);

            //Assert.Equal(actualResult, expectedResult);
        }

        [Fact]
        public async Task GetAllRestaurants_VerifyToGetAllTheRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    ID = 1,
                    RestaurantName = "Domino's",
                    CuisineType = "Italian"
                },
                new Restaurant
                {
                    ID = 2,
                    RestaurantName = "Blue Mug",
                    CuisineType = "Continental"
                }
            };

            _dataRepository.Setup(s => s.GetAll<Restaurant>()).Returns(restaurants.AsQueryable().BuildMock().Object);

            var actualResult = await _unitOfWorkRepository.Restaurant.GetAllRestaurants();
            //var afterResult = 
        }

        [Fact]
        public async Task GetUserRestaurants_VerifyToGetRestaurantsAsPerUser()
        {
            List<Restaurant> restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    ID = 1,
                    RestaurantName = "Domino's",
                    CuisineType = "Italian",
                    Dishes = new List<Dishes>
                    {
                        new Dishes
                        {
                            ID = 1,
                            DishesName = "Pizza",
                            Costs = 250
                        }
                    }
                },
                new Restaurant
                {
                    ID = 2,
                    RestaurantName = "Blue Mug",
                    CuisineType = "Continental",
                    Dishes = new List<Dishes>
                    {
                        new Dishes
                        {
                            ID = 1,
                            DishesName = "Taco",
                            Costs = 150
                        }
                    }
                }
            };
            _dataRepository.Setup(s => s.Where(It.IsAny<Expression<Func<Restaurant, bool>>>())).Returns(restaurants.AsQueryable().BuildMock().Object);
            await _unitOfWorkRepository.Restaurant.GetUserRestaurants(1);
        }

        [Fact]
        public async Task GetDishes_VerifyToGetAllTheDishesForTheRestaurant()
        {
            List<Restaurant> restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    ID = 1,
                    RestaurantName = "Domino's",
                    CuisineType = "Italian",
                    Dishes = new List<Dishes>
                    {
                        new Dishes
                        {
                            ID = 1,
                            DishesName = "Pizza",
                            Costs = 250
                        },
                        new Dishes
                        {
                            ID = 2,
                            DishesName = "Taco",
                            Costs = 150
                        }
                    }
                },
            };
            _dataRepository.Setup(s => s.GetAll<Restaurant>()).Returns(restaurants.AsQueryable().BuildMock().Object);
            await _unitOfWorkRepository.Restaurant.GetDishes(1);
        }
        //________________________________________________________________________________________________________________________________________________________________________

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

            List<Country> countries = new List<Country>
            {
                new Country
                {
                    ID = 1,
                    CountryName = "India"
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
            _dataRepository.Setup(s => s.GetAll<Country>()).Returns(countries.AsQueryable().BuildMock().Object);
            _dataRepository.Setup(s => s.AddRangeAsync(It.IsAny<IEnumerable<Location>>()));
            _dataRepository.Setup(s => s.SaveChangesAsync());

            await _unitOfWorkRepository.Restaurant.AddLocation(locationACs);

            _dataRepository.Verify(v => v.AddRangeAsync(It.IsAny<IEnumerable<City>>()), Times.Once);
            _dataRepository.Verify(v => v.SaveChangesAsync());
        }

        [Fact]
        public async Task AddOrderDetails_PostUsersAndCreateOrderDetails()
        {
            List<User> users = new List<User>
            {
                new User
                {
                    Id = "",
                    UserName = ""
                }
            };

            List<Dishes> dishes = new List<Dishes>
            {
                new Dishes
                {
                    ID = 1,
                    DishesName = "Burger",
                    Costs = 250
                }
            };

            OrderDetailsAC orderDetailsACs = new OrderDetailsAC
            {
                RestaurantID = 1,
                UserID = "",
                UserName = "Olivia Roy",
                DishesOrdered = new List<DishesOrdered>
                {
                    new DishesOrdered
                    {
                        ID = 1,
                        ItemsCount = 4,
                        Dishes = new Dishes
                        {
                             ID = 1,
                             DishesName = "burger",
                             Costs = 200
                        }
                    }
                }
            };

            _dataRepository.Setup(s => s.FirstAsync(It.IsAny<Expression<Func<Restaurant, bool>>>()));
            _dataRepository.Setup(s => s.Where(It.IsAny<Expression<Func<User, bool>>>())).Returns(users.AsQueryable().BuildMock().Object);
            _dataRepository.Setup(s => s.GetAll<Dishes>()).Returns(dishes.AsQueryable().BuildMock().Object);
            _dataRepository.Setup(s => s.AddAsync(It.IsAny<Expression<Func<OrderDetails, bool>>>()));
            _dataRepository.Setup(s => s.AddRangeAsync(It.IsAny<IEnumerable<OrderDetails>>()));
            _dataRepository.Setup(s => s.SaveChangesAsync());

            await _unitOfWorkRepository.Restaurant.AddOrderDetails(orderDetailsACs);

            _dataRepository.Verify(v => v.AddAsync(It.IsAny<Dishes>()), Times.Once);
            _dataRepository.Verify(v => v.AddRangeAsync(It.IsAny<IEnumerable<OrderDetailsAC>>()));
            _dataRepository.Verify(v => v.SaveChangesAsync());
        }

        [Fact]
        public async Task AddAllRestaurants_VerifyAdditionOfRestaurantsWithDetails()
        {
            AllDetails allDetails = new AllDetails
            {
                LocationID = 1,
                RestaurantID = 2,
                RestaurantName = "Domino's",
                ContactNumber = "68886888",
                CuisineType = "Italian",
                AverageCost = "500",
                OpeningHours = "10AM - 11PM",
                MoreInfo = "Pet friendly",
                Locations = new List<Location>
                {
                    new Location
                    {
                        ID = 1,
                        CityID = 2,
                        CountryID = 1,
                        Locality = "Manjalpur",
                        City = new City
                        {
                            ID = 1,
                            CityName = "Kolkata"
                        },
                        Country = new Country
                        {
                            ID = 1,
                            CountryName = "India"
                        }
                    }
                },
                Reviews = new List<ReviewsAC>
                {
                    new ReviewsAC
                    {
                       userID = "",
                       UserName = "Olivia Roy",
                       LikesCount = 4,
                       ReviewId = 1007,
                       ReviewTexts = "erjk",
                       commentACs = new List<CommentAC>
                       {
                            new CommentAC
                            {
                                ID = 1,
                                UserID = "",
                                FullName = "Olivia Roy",
                                CommentMessage = "good",
                                ReviewID = 2
                            }
                       }
                    }
                },
                Comments = new List<CommentAC>
                {
                    new CommentAC
                    {
                        ID = 1,
                        UserID = "",
                        FullName = "Olivia Roy",
                        CommentMessage = "good",
                        ReviewID = 2
                    }
                }
            };
            _dataRepository.Setup(s => s.AddAsync(It.IsAny<Expression<Func<Restaurant, bool>>>()));//.Returns(restaurant.AsQueryable().BuildMock().Object);
            _dataRepository.Setup(s => s.AddAsync(It.IsAny<Expression<Func<City, bool>>>()));
            _dataRepository.Setup(s => s.AddAsync(It.IsAny<Expression<Func<Country, bool>>>()));
            _dataRepository.Setup(s => s.AddAsync(It.IsAny<Expression<Func<Location, bool>>>()));
            _dataRepository.Setup(s => s.SaveChangesAsync());

            await _unitOfWorkRepository.Restaurant.AddAllRestaurants(allDetails);

            _dataRepository.Verify(v => v.AddAsync(It.IsAny<Dishes>()), Times.Once);
            _dataRepository.Verify(v => v.SaveChangesAsync());
        }

        [Fact]
        public async Task NewDish_VerifyWhetherANewDishIsAddedToARestaurant()
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

            _dataRepository.Setup(s => s.Where(It.IsAny<Expression<Func<Restaurant, bool>>>())).Returns(newResturant.AsQueryable().BuildMock().Object);
            _dataRepository.Setup(s => s.AddAsync(It.IsAny<Expression<Func<Dishes, bool>>>()));

            await _unitOfWorkRepository.Restaurant.NewDish(1, newDishes);

            _dataRepository.Verify(v => v.AddAsync(It.IsAny<Dishes>()), Times.Once);
            _dataRepository.Verify(v => v.SaveChangesAsync());
        }


        [Fact]
        public async Task AddReviews_VerifyIfAReviewIsAddedToARestaurant()
        {
            ReviewsAC ReviewsAC = new ReviewsAC
            {
                ReviewId = 1,
                ReviewTexts = "Good",
                userID = "",
                LikesCount = 4,
                UserName = "Olivia Roy",
                commentACs = new List<CommentAC>
                {
                    new CommentAC
                    {
                        ID = 1,
                        ReviewID = 1,
                        UserID = "",
                        FullName = "Olivia Roy",
                        CommentMessage = "Okay"
                    }
                }
            };

            List<Restaurant> restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    ID = 1
                }
            };
            List<User> users = new List<User>
            {
                new User
                {
                    Id = "",
                    UserName = ""
                }
            };


            _dataRepository.Setup(s => s.Where(It.IsAny<Expression<Func<Restaurant, bool>>>())).Returns(restaurants.AsQueryable().BuildMock().Object);
            _dataRepository.Setup(s => s.Where(It.IsAny<Expression<Func<User, bool>>>())).Returns(users.AsQueryable().BuildMock().Object);
            _dataRepository.Setup(s => s.AddAsync(It.IsAny<Expression<Func<Review, bool>>>()));
            _dataRepository.Setup(s => s.SaveChangesAsync());

            await _unitOfWorkRepository.Restaurant.AddReviews(1, ReviewsAC);

            _dataRepository.Verify(v => v.AddAsync(It.IsAny<ReviewsAC>()), Times.Once);
            _dataRepository.Verify(v => v.SaveChangesAsync());
        }

        [Fact]
        public async Task Likes_VerifyWhetherAReviewPostIsGettingLikedOrNot()
        {
            List<Restaurant> restaurants = new List<Restaurant>
            {
                new Restaurant
                {

                }
            };

            List<User> users = new List<User>
            {
                new User
                {

                }
            };

            ReviewsAC ReviewsAC = new ReviewsAC
            {
                ReviewId = 1,
                ReviewTexts = "Good",
                userID = "",
                LikesCount = 4,
                UserName = "Olivia Roy",
                commentACs = new List<CommentAC>
                {
                    new CommentAC
                    {
                        ID = 1,
                        ReviewID = 1,
                        UserID = "",
                        FullName = "Olivia Roy",
                        CommentMessage = "Okay"
                    }
                }
            };
            _dataRepository.Setup(s => s.Where(It.IsAny<Expression<Func<Restaurant, bool>>>())).Returns(restaurants.AsQueryable().BuildMock().Object);
            _dataRepository.Setup(s => s.Where(It.IsAny<Expression<Func<User, bool>>>())).Returns(users.AsQueryable().BuildMock().Object);
            _dataRepository.Setup(s => s.AddAsync(It.IsAny<Expression<Func<Review, bool>>>()));
            _dataRepository.Setup(s => s.SaveChangesAsync());

            await _unitOfWorkRepository.Restaurant.Likes(1, "");

            //_dataRepository.Verify(v => v.AddAsync(It.IsAny<Review>()), Times.Once);
            _dataRepository.Verify(v => v.SaveChangesAsync());
        }

        [Fact]
        public async Task CommentSection_VerifyAdditionMultipleCommentsOnAReview()
        {
            List<Restaurant> restaurants = new List<Restaurant>
            {
                new Restaurant
                {

                }
            };

            List<Review> reviews = new List<Review>
            {
                new Review
                {

                }
            };

            CommentAC commentAC = new CommentAC
            {
                ID = 1,
                UserID = "",
                ReviewID = 1,
                FullName = "Olivia Roy",
                CommentMessage = "Okay"
            };
            _dataRepository.Setup(s => s.Where(It.IsAny<Expression<Func<Restaurant, bool>>>())).Returns(restaurants.AsQueryable().BuildMock().Object);
            _dataRepository.Setup(s => s.Where(It.IsAny<Expression<Func<Review, bool>>>())).Returns(reviews.AsQueryable().BuildMock().Object);
            _dataRepository.Setup(s => s.AddAsync(It.IsAny<Expression<Func<Comment, bool>>>()));
            _dataRepository.Setup(s => s.SaveChangesAsync());

            await _unitOfWorkRepository.Restaurant.CommentSection(1, commentAC);

            _dataRepository.Verify(v => v.AddAsync(It.IsAny<CommentAC>()), Times.Once);
            _dataRepository.Verify(v => v.SaveChangesAsync());
        }
        //________________________________________________________________________________________________________________________________________________________________________

        //Task<AllDetails> EditRestaurant(int id, AllDetails details);
        [Fact]
        public async Task EditRestaurant_VerifyWhetherArestaurantCanBeEditedOrNot()
        {
            List<Restaurant> restaurants = new List<Restaurant>
            {
                new Restaurant
                {

                }
            };

            List<Review> reviews = new List<Review>
            {
                new Review
                {

                }
            };

            CommentAC commentAC = new CommentAC
            {
                ID = 1,
                UserID = "",
                ReviewID = 1,
                FullName = "Olivia Roy",
                CommentMessage = "Okay"
            };
            _dataRepository.Setup(s => s.Where(It.IsAny<Expression<Func<Restaurant, bool>>>())).Returns(restaurants.AsQueryable().BuildMock().Object);
            _dataRepository.Setup(s => s.Where(It.IsAny<Expression<Func<Review, bool>>>())).Returns(reviews.AsQueryable().BuildMock().Object);
            _dataRepository.Setup(s => s.AddAsync(It.IsAny<Expression<Func<Comment, bool>>>()));
            _dataRepository.Setup(s => s.SaveChangesAsync());

            await _unitOfWorkRepository.Restaurant.CommentSection(1, commentAC);

            _dataRepository.Verify(v => v.AddAsync(It.IsAny<CommentAC>()), Times.Once);
            _dataRepository.Verify(v => v.SaveChangesAsync());
        }
        //________________________________________________________________________________________________________________________________________________________________________

        [Fact]
        public async Task DeleteRestaurant_VerifyWhetherARestaurantIsGettingDeletedOrNot()
        {
            List<Restaurant> restaurants = new List<Restaurant>
            {
                new Restaurant
                {

                }
            };
            List<OrderDetails> orderDetails = new List<OrderDetails>
            {
                new OrderDetails
                {

                }
            };
            List<Likes> likes = new List<Likes>
            {
                new Likes
                {

                }
            };
            List<Comment> comments = new List<Comment>
            {
                new Comment
                {

                }
            };
            List<Review> reviews = new List<Review>
            {
                new Review
                {

                }
            };

            _dataRepository.Setup(s => s.Where(It.IsAny<Expression<Func<Restaurant, bool>>>())).Returns(restaurants.AsQueryable().BuildMock().Object);
            _dataRepository.Setup(s => s.Where(It.IsAny<Expression<Func<OrderDetails, bool>>>())).Returns(orderDetails.AsQueryable().BuildMock().Object);
            _dataRepository.Setup(s => s.Where(It.IsAny<Expression<Func<Likes, bool>>>())).Returns(likes.AsQueryable().BuildMock().Object);
            _dataRepository.Setup(s => s.Where(It.IsAny<Expression<Func<Comment, bool>>>())).Returns(comments.AsQueryable().BuildMock().Object);
            _dataRepository.Setup(s => s.Where(It.IsAny<Expression<Func<Review, bool>>>())).Returns(reviews.AsQueryable().BuildMock().Object);

            //_dataRepository.Setup(s => s.RemoveRange(It.IsAny<IEnumerable<Review>>()));
            //_dataRepository.Setup(s => s.RemoveRange(It.IsAny<IEnumerable<DishesOrdered>>()));
            //_dataRepository.Setup(s => s.RemoveRange(It.IsAny<IEnumerable<Comment>>()));
            //_dataRepository.Setup(s => s.RemoveRange(It.IsAny<IEnumerable<Likes>>()));
            //_dataRepository.Setup(s => s.RemoveRange(It.IsAny<IEnumerable<OrderDetails>>()));
            //_dataRepository.Setup(s => s.RemoveRange(It.IsAny<IEnumerable<Dishes>>()));
            //_dataRepository.Setup(s => s.Remove(It.IsAny<IEnumerable<Restaurant>>()));

            await _unitOfWorkRepository.Restaurant.DeleteRestaurant(1);

            _dataRepository.Verify(v => v.RemoveRange(It.IsAny<IEnumerable<Review>>()));
            _dataRepository.Verify(v => v.RemoveRange(It.IsAny<IEnumerable<DishesOrdered>>()));
            _dataRepository.Verify(v => v.RemoveRange(It.IsAny<IEnumerable<Comment>>()));
            _dataRepository.Verify(v => v.RemoveRange(It.IsAny<IEnumerable<Likes>>()));
            _dataRepository.Verify(v => v.RemoveRange(It.IsAny<IEnumerable<OrderDetails>>()));
            _dataRepository.Verify(v => v.RemoveRange(It.IsAny<IEnumerable<Dishes>>()));
            _dataRepository.Verify(v => v.Remove(It.IsAny<IEnumerable<Restaurant>>()));

        }

        [Fact]
        public async Task DeleteDishes_VerifyIfADishOfAPerticularRestaurantIsGettingDeleted()
        {
            List<DishesOrdered> dishesOrdered = new List<DishesOrdered>
            {
                new DishesOrdered
                {

                }
            };
            List<OrderDetails> orderDetails = new List<OrderDetails>
            {
                new OrderDetails
                {

                }
            };

            _dataRepository.Setup(s => s.FindAsyncById<Dishes>(1));
            _dataRepository.Setup(s => s.Where(It.IsAny<Expression<Func<DishesOrdered, bool>>>())).Returns(dishesOrdered.AsQueryable().BuildMock().Object);
            _dataRepository.Setup(s => s.GetAll<OrderDetails>()).Returns(orderDetails.AsQueryable().BuildMock().Object);

            await _unitOfWorkRepository.Restaurant.DeleteDishes(1);

            _dataRepository.Verify(v => v.Remove(It.IsAny<IEnumerable<Dishes>>()));
            _dataRepository.Verify(v => v.RemoveRange(It.IsAny<IEnumerable<OrderDetails>>()));

        }
    }
}

