using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZomatoDemo.DomainModel.Application_Classes;
using ZomatoDemo.DomainModel.Models;

namespace ZomatoDemo.Web.Models
{
    public class ZomatoDbContext : IdentityDbContext<User>
    {
        public ZomatoDbContext (DbContextOptions<ZomatoDbContext> options) : base(options)
        {
        }

        public DbSet<City> City { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Dishes> Dishes { get; set; }
        public DbSet<DishesOrdered> DishesOrdered { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<Review> Review { get; set; }
        //public DbSet<UserAC> User { get; set; }
        public DbSet<UserFollow> UserFollow { get; set; }

    }

}
