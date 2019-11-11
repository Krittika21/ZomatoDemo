using Microsoft.EntityFrameworkCore.Migrations;

namespace ZomatoDemo.DomainModel.Migrations
{
    public partial class CommentsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
