using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NerdStore.Catalog.Data.Migrations
{
    public partial class ProductTableChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Products");
        }
    }
}
