using Microsoft.EntityFrameworkCore.Migrations;

namespace Shoplister.Migrations
{
    public partial class productType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductType",
                table: "Items",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductType",
                table: "Items");
        }
    }
}
