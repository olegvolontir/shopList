using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopList.Migrations
{
    public partial class CartProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_cart_CartEntityId",
                table: "product");

            migrationBuilder.DropIndex(
                name: "IX_product_CartEntityId",
                table: "product");

            migrationBuilder.DropColumn(
                name: "CartEntityId",
                table: "product");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "cart",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "cart_product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cart_product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cart_product_cart_Id",
                        column: x => x.Id,
                        principalTable: "cart",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cart_product_product_Id",
                        column: x => x.Id,
                        principalTable: "product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cart_AspNetUsers_UserId",
                table: "cart");

            migrationBuilder.DropTable(
                name: "cart_product");

            migrationBuilder.DropIndex(
                name: "IX_cart_UserId",
                table: "cart");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "cart");

            migrationBuilder.AddColumn<int>(
                name: "CartEntityId",
                table: "product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_CartEntityId",
                table: "product",
                column: "CartEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_product_cart_CartEntityId",
                table: "product",
                column: "CartEntityId",
                principalTable: "cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
