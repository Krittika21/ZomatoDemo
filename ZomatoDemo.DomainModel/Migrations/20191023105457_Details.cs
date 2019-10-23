using Microsoft.EntityFrameworkCore.Migrations;

namespace ZomatoDemo.DomainModel.Migrations
{
    public partial class Details : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AverageCost",
                table: "Restaurant",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConatctNumber",
                table: "Restaurant",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CuisineType",
                table: "Restaurant",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Restaurant",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MoreInfo",
                table: "Restaurant",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OpeningHours",
                table: "Restaurant",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Costs",
                table: "Dishes",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageCost",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "ConatctNumber",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "CuisineType",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "MoreInfo",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "OpeningHours",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "Costs",
                table: "Dishes");
        }
    }
}
