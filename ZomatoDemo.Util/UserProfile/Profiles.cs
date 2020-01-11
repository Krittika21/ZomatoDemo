using AutoMapper;
using ZomatoDemo.DomainModel.Application_Classes;
using ZomatoDemo.DomainModel.Models;

namespace ZomatoDemo.DomainModel.UserProfile
{
    public class Profiles : Profile
    {
       public Profiles()
        {
            CreateMap<City, AllCity>().ReverseMap();
            CreateMap<Country, AllCountry>().ReverseMap();

            CreateMap<Restaurant, AllDetails>().ForMember(destination => destination.RestaurantID,
               opts => opts.MapFrom(source => source.ID)).ReverseMap();

            CreateMap<Location, AllDetails>().ForMember(destination => destination.LocationID,
            acts => acts.MapFrom(source => source.ID)).ReverseMap();

            //CreateMap<City, AllDetails>().ReverseMap();
            CreateMap<Country, AllDetails>().ReverseMap();
            CreateMap<Review, AllDetails>().ReverseMap();

            CreateMap<Dishes, AllDishes>().ReverseMap();
            CreateMap<Restaurant, AllRestaurants>();
            //CommentAC
            CreateMap<Comment, CommentAC>().ReverseMap();
            CreateMap<User, CommentAC>();

            CreateMap<User, CurrentUser>();
            CreateMap<Location, LocationAC>();
            CreateMap<User, LoginAC>();
            CreateMap<OrderDetails, OrderDetailsAC>().ReverseMap();
            //RestaurantAC
            CreateMap<User, RegisterAC>();
            CreateMap<Restaurant, RestaurantAC>();
            CreateMap<Dishes, RestaurantAC>();
            CreateMap<Location, RestaurantAC>();

            CreateMap<OrderDetailsAC, User>().ReverseMap();
            CreateMap<OrderDetailsAC, Restaurant>().ReverseMap();
            CreateMap<OrderDetailsAC, DishesOrdered>().ReverseMap();

            CreateMap<Review, ReviewsAC>().ForMember(destination => destination.ReviewId,
           opts => opts.MapFrom(source => source.ID)).ReverseMap();

            CreateMap<ReviewsAC, User>().ForMember(destination => destination.UserName,
           opts => opts.MapFrom(source => source.UserName)).ForMember(destination => destination.Id,
           opts => opts.MapFrom(source => source.userID));
        }
    }
}
