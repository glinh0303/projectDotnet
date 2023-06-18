using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Data.Migrations
{
    public partial class OrderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderDetailId",
                table: "Topping",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Topping",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Topping_OrderDetailId",
                table: "Topping",
                column: "OrderDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Topping_OrderDetail_OrderDetailId",
                table: "Topping",
                column: "OrderDetailId",
                principalTable: "OrderDetail",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topping_OrderDetail_OrderDetailId",
                table: "Topping");

            migrationBuilder.DropIndex(
                name: "IX_Topping_OrderDetailId",
                table: "Topping");

            migrationBuilder.DropColumn(
                name: "OrderDetailId",
                table: "Topping");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Topping");
        }
    }
}
