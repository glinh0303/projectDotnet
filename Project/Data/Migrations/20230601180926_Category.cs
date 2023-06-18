using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Data.Migrations
{
    public partial class Category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryDrink");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Drink",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Drink",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.CreateTable(
                name: "CategoryDrink",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    DrinksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryDrink", x => new { x.CategoriesId, x.DrinksId });
                    table.ForeignKey(
                        name: "FK_CategoryDrink_Category_CategoriesId",
                        column: x => x.CategoriesId,
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
                name: "IX_CategoryDrink_DrinksId",
                table: "CategoryDrink",
                column: "DrinksId");
        }
    }
}
