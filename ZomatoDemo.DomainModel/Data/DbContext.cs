using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZomatoDemo.DomainModel.Models;

namespace ZomatoDemo.Web.Models
{
    public class ZomatoDbContext : DbContext
    {
        public ZomatoDbContext (DbContextOptions<ZomatoDbContext> options) : base(options)
        {
        }

        public DbSet<City> City { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Dishes> Dishes { get; set; }
        public DbSet<DishesOrdered> DishesOrdered { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<Restaurant> Restaurant { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserFollow> UserFollow { get; set; }
    }
}
