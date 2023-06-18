using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Data.Migrations
{
    public partial class updateDrink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drink_Category_CategoryId",
                table: "Drink");

            migrationBuilder.DropIndex(
                name: "IX_Drink_CategoryId",
                table: "Drink");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Drink");

            migrationBuilder.CreateTable(
                name: "CategoryDrink",
                columns: table => new
                {
                    DrinksId = table.Column<int>(type: "int", nullable: false),
                    TypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryDrink", x => new { x.DrinksId, x.TypesId });
                    table.ForeignKey(
                        name: "FK_CategoryDrink_Category_TypesId",
                        column: x => x.TypesId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryDrink_Drink_DrinksId",
                        column: x => x.DrinksId,
                        principalTable: "Drink",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryDrink_TypesId",
                table: "CategoryDrink",
                column: "TypesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryDrink");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Drink",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drink_CategoryId",
                table: "Drink",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drink_Category_CategoryId",
                table: "Drink",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }
    }
}
