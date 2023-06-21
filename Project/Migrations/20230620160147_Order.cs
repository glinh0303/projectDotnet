using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    public partial class Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfileUserId",
                table: "OrderDetail",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ProfileUserId",
                table: "OrderDetail",
                column: "ProfileUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Profile_ProfileUserId",
                table: "OrderDetail",
                column: "ProfileUserId",
                principalTable: "Profile",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Profile_ProfileUserId",
                table: "OrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetail_ProfileUserId",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "ProfileUserId",
                table: "OrderDetail");
        }
    }
}
