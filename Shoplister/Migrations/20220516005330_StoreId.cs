using Microsoft.EntityFrameworkCore.Migrations;

namespace Shoplister.Migrations
{
    public partial class StoreId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemStore_Stores_StoreId",
                table: "ItemStore");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ItemStore");

            migrationBuilder.AlterColumn<int>(
                name: "StoreId",
                table: "ItemStore",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemStore_Stores_StoreId",
                table: "ItemStore",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemStore_Stores_StoreId",
                table: "ItemStore");

            migrationBuilder.AlterColumn<int>(
                name: "StoreId",
                table: "ItemStore",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ItemStore",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemStore_Stores_StoreId",
                table: "ItemStore",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
