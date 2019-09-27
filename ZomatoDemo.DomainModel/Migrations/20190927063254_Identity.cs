using Microsoft.EntityFrameworkCore.Migrations;

namespace ZomatoDemo.DomainModel.Migrations
{
    public partial class Identity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DishesOrdered_OrderDetails_OrderID",
                table: "DishesOrdered");

            migrationBuilder.RenameColumn(
                name: "OrderID",
                table: "DishesOrdered",
                newName: "OrderDetailsID");

            migrationBuilder.RenameIndex(
                name: "IX_DishesOrdered_OrderID",
                table: "DishesOrdered",
                newName: "IX_DishesOrdered_OrderDetailsID");

            migrationBuilder.AddForeignKey(
                name: "FK_DishesOrdered_OrderDetails_OrderDetailsID",
                table: "DishesOrdered",
                column: "OrderDetailsID",
                principalTable: "OrderDetails",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DishesOrdered_OrderDetails_OrderDetailsID",
                table: "DishesOrdered");

            migrationBuilder.RenameColumn(
                name: "OrderDetailsID",
                table: "DishesOrdered",
                newName: "OrderID");

            migrationBuilder.RenameIndex(
                name: "IX_DishesOrdered_OrderDetailsID",
                table: "DishesOrdered",
                newName: "IX_DishesOrdered_OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_DishesOrdered_OrderDetails_OrderID",
                table: "DishesOrdered",
                column: "OrderID",
                principalTable: "OrderDetails",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
