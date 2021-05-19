using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopList.Migrations
{
    public partial class CartProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProductEntity_cart_Id",
                table: "CartProductEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_CartProductEntity_product_Id",
                table: "CartProductEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartProductEntity",
                table: "CartProductEntity");

            migrationBuilder.RenameTable(
                name: "CartProductEntity",
                newName: "cart_product");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cart_product",
                table: "cart_product",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_cart_product_cart_Id",
                table: "cart_product",
                column: "Id",
                principalTable: "cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cart_product_product_Id",
                table: "cart_product",
                column: "Id",
                principalTable: "product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cart_product_cart_Id",
                table: "cart_product");

            migrationBuilder.DropForeignKey(
                name: "FK_cart_product_product_Id",
                table: "cart_product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cart_product",
                table: "cart_product");

            migrationBuilder.RenameTable(
                name: "cart_product",
                newName: "CartProductEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartProductEntity",
                table: "CartProductEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProductEntity_cart_Id",
                table: "CartProductEntity",
                column: "Id",
                principalTable: "cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartProductEntity_product_Id",
                table: "CartProductEntity",
                column: "Id",
                principalTable: "product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
