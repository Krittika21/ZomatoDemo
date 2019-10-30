using Microsoft.EntityFrameworkCore.Migrations;

namespace ZomatoDemo.DomainModel.Migrations
{
    public partial class CascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_Dishes_Restaurant_RestaurantID", "Dishes");
            migrationBuilder.AddForeignKey(
                        name: "FK_Dishes_Restaurant_RestaurantID",
                        table: "Dishes",
                        column: "RestaurantID",
                        principalTable: "Restaurant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropForeignKey("FK_Location_Restaurant_RestaurantID", "Location");
            migrationBuilder.AddForeignKey(
                        name: "FK_Location_Restaurant_RestaurantID",
                        table: "Location",
                        column: "RestaurantID",
                        principalTable: "Restaurant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
