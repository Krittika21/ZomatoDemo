using Microsoft.EntityFrameworkCore.Migrations;

namespace ZomatoDemo.DomainModel.Migrations
{
    public partial class ContactSpelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConatctNumber",
                table: "Restaurant",
                newName: "ContactNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContactNumber",
                table: "Restaurant",
                newName: "ConatctNumber");
        }
    }
}
