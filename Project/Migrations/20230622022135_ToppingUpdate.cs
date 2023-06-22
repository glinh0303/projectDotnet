using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    public partial class ToppingUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "OrderDetailTopping",
                columns: table => new
                {
                    OrderDetailsId = table.Column<int>(type: "int", nullable: false),
                    ToppingsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailTopping", x => new { x.OrderDetailsId, x.ToppingsId });
                    table.ForeignKey(
                        name: "FK_OrderDetailTopping_OrderDetail_OrderDetailsId",
                        column: x => x.OrderDetailsId,
                        principalTable: "OrderDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetailTopping_Topping_ToppingsId",
                        column: x => x.ToppingsId,
                        principalTable: "Topping",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailTopping_ToppingsId",
                table: "OrderDetailTopping",
                column: "ToppingsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetailTopping");

            migrationBuilder.AddColumn<int>(
                name: "OrderDetailId",
                table: "Topping",
                type: "int",
                nullable: true);

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
    }
}
