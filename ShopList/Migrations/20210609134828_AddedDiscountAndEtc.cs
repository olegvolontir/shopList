using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopList.Migrations
{
    public partial class AddedDiscountAndEtc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cart_AspNetUsers_UserId",
                table: "cart");

            migrationBuilder.DropIndex(
                name: "IX_cart_UserId",
                table: "cart");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "cart");

            migrationBuilder.AddColumn<float>(
                name: "Discount",
                table: "product",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CartId",
                table: "AspNetUsers",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_cart_CartId",
                table: "AspNetUsers",
                column: "CartId",
                principalTable: "cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_cart_CartId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CartId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "product");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "cart",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_cart_UserId",
                table: "cart",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_cart_AspNetUsers_UserId",
                table: "cart",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
