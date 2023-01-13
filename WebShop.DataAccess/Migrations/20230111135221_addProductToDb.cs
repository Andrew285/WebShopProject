using Microsoft.EntityFrameworkCore.Migrations;

namespace WebShop.DataAccess.Migrations
{
    public partial class addProductToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SprecialPrice5",
                table: "Products",
                newName: "SpecialPrice5");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpecialPrice5",
                table: "Products",
                newName: "SprecialPrice5");
        }
    }
}
