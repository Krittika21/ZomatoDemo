﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZomatoDemo.Web.Models;

namespace ZomatoDemo.DomainModel.Migrations
{
    [DbContext(typeof(ZomatoDbContext))]
    [Migration("20190920044741_restaurant")]
    partial class restaurant
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ZomatoDemo.DomainModel.Models.City", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CityName");

                    b.HasKey("ID");

                    b.ToTable("City");
                });

            modelBuilder.Entity("ZomatoDemo.DomainModel.Models.Country", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CountryName");

                    b.HasKey("ID");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("ZomatoDemo.DomainModel.Models.Dishes", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DishesName");

                    b.Property<int?>("RestaurantID");

                    b.HasKey("ID");

                    b.HasIndex("RestaurantID");

                    b.ToTable("Dishes");
                });

            modelBuilder.Entity("ZomatoDemo.DomainModel.Models.DishesOrdered", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DishesID");

                    b.Property<int>("ItemsCount");

                    b.Property<int?>("OrderID");

                    b.HasKey("ID");

                    b.HasIndex("DishesID");

                    b.HasIndex("OrderID");

                    b.ToTable("DishesOrdered");
                });

            modelBuilder.Entity("ZomatoDemo.DomainModel.Models.Likes", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ReviewsID");

                    b.Property<int?>("UsersID");

                    b.HasKey("ID");

                    b.HasIndex("ReviewsID");

                    b.HasIndex("UsersID");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("ZomatoDemo.DomainModel.Models.Location", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CityID");

                    b.Property<int?>("CountryID");

                    b.Property<string>("Locality");

                    b.Property<int?>("RestaurantID");

                    b.HasKey("ID");

                    b.HasIndex("CityID");

                    b.HasIndex("CountryID");

                    b.HasIndex("RestaurantID");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("ZomatoDemo.DomainModel.Models.OrderDetails", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTime");

                    b.Property<int?>("RestaurantID");

                    b.Property<int?>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("RestaurantID");

                    b.HasIndex("UserID");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("ZomatoDemo.DomainModel.Models.Rating", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("AverageRating");

                    b.Property<int?>("RestaurantID");

                    b.Property<int?>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("RestaurantID");

                    b.HasIndex("UserID");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("ZomatoDemo.DomainModel.Models.Restaurant", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RestaurantName");

                    b.HasKey("ID");

                    b.ToTable("Restaurant");
                });

            modelBuilder.Entity("ZomatoDemo.DomainModel.Models.Review", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LikesCount");

                    b.Property<int?>("RestaurantID");

                    b.Property<string>("ReviewTexts");

                    b.Property<int?>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("RestaurantID");

                    b.HasIndex("UserID");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("ZomatoDemo.DomainModel.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ZomatoDemo.DomainModel.Models.UserFollow", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("FolloweeID");

                    b.Property<int?>("FollowerID");

                    b.HasKey("ID");

                    b.HasIndex("FolloweeID");

                    b.HasIndex("FollowerID");

                    b.ToTable("UserFollow");
                });

            modelBuilder.Entity("ZomatoDemo.DomainModel.Models.Dishes", b =>
                {
                    b.HasOne("ZomatoDemo.DomainModel.Models.Restaurant")
                        .WithMany("Dishes")
                        .HasForeignKey("RestaurantID");
                });

            modelBuilder.Entity("ZomatoDemo.DomainModel.Models.DishesOrdered", b =>
                {
                    b.HasOne("ZomatoDemo.DomainModel.Models.Dishes", "Dishes")
                        .WithMany()
                        .HasForeignKey("DishesID");

                    b.HasOne("ZomatoDemo.DomainModel.Models.OrderDetails", "Order")
                        .WithMany()
                        .HasForeignKey("OrderID");
                });

            modelBuilder.Entity("ZomatoDemo.DomainModel.Models.Likes", b =>
                {
                    b.HasOne("ZomatoDemo.DomainModel.Models.Review", "Reviews")
                        .WithMany()
                        .HasForeignKey("ReviewsID");

                    b.HasOne("ZomatoDemo.DomainModel.Models.User", "Users")
                        .WithMany()
                        .HasForeignKey("UsersID");
                });

            modelBuilder.Entity("ZomatoDemo.DomainModel.Models.Location", b =>
                {
                    b.HasOne("ZomatoDemo.DomainModel.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityID");

                    b.HasOne("ZomatoDemo.DomainModel.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryID");

                    b.HasOne("ZomatoDemo.DomainModel.Models.Restaurant")
                        .WithMany("Location")
                        .HasForeignKey("RestaurantID");
                });

            modelBuilder.Entity("ZomatoDemo.DomainModel.Models.OrderDetails", b =>
                {
                    b.HasOne("ZomatoDemo.DomainModel.Models.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantID");

                    b.HasOne("ZomatoDemo.DomainModel.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("ZomatoDemo.DomainModel.Models.Rating", b =>
                {
                    b.HasOne("ZomatoDemo.DomainModel.Models.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantID");

                    b.HasOne("ZomatoDemo.DomainModel.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("ZomatoDemo.DomainModel.Models.Review", b =>
                {
                    b.HasOne("ZomatoDemo.DomainModel.Models.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantID");

                    b.HasOne("ZomatoDemo.DomainModel.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("ZomatoDemo.DomainModel.Models.UserFollow", b =>
                {
                    b.HasOne("ZomatoDemo.DomainModel.Models.User", "Followee")
                        .WithMany()
                        .HasForeignKey("FolloweeID");

                    b.HasOne("ZomatoDemo.DomainModel.Models.User", "Follower")
                        .WithMany()
                        .HasForeignKey("FollowerID");
                });
#pragma warning restore 612, 618
        }
    }
}
