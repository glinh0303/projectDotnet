using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Data.Migrations
{
    public partial class UpdateDrinkCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryDrink_Category_TypesId",
                table: "CategoryDrink");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryDrink",
                table: "CategoryDrink");

            migrationBuilder.DropIndex(
                name: "IX_CategoryDrink_TypesId",
                table: "CategoryDrink");

            migrationBuilder.RenameColumn(
                name: "TypesId",
                table: "CategoryDrink",
                newName: "CategoriesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryDrink",
                table: "CategoryDrink",
                columns: new[] { "CategoriesId", "DrinksId" });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryDrink_DrinksId",
                table: "CategoryDrink",
                column: "DrinksId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryDrink_Category_CategoriesId",
                table: "CategoryDrink",
                column: "CategoriesId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryDrink_Category_CategoriesId",
                table: "CategoryDrink");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryDrink",
                table: "CategoryDrink");

            migrationBuilder.DropIndex(
                name: "IX_CategoryDrink_DrinksId",
                table: "CategoryDrink");

            migrationBuilder.RenameColumn(
                name: "CategoriesId",
                table: "CategoryDrink",
                newName: "TypesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryDrink",
                table: "CategoryDrink",
                columns: new[] { "DrinksId", "TypesId" });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryDrink_TypesId",
                table: "CategoryDrink",
                column: "TypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryDrink_Category_TypesId",
                table: "CategoryDrink",
                column: "TypesId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
