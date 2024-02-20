using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobileStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductContents_Products_ContentId",
                table: "ProductContents");

            migrationBuilder.DropIndex(
                name: "IX_ProductContents_ContentId",
                table: "ProductContents");

            migrationBuilder.CreateIndex(
                name: "IX_ProductContents_ProductId",
                table: "ProductContents",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductContents_Products_ProductId",
                table: "ProductContents",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductContents_Products_ProductId",
                table: "ProductContents");

            migrationBuilder.DropIndex(
                name: "IX_ProductContents_ProductId",
                table: "ProductContents");

            migrationBuilder.CreateIndex(
                name: "IX_ProductContents_ContentId",
                table: "ProductContents",
                column: "ContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductContents_Products_ContentId",
                table: "ProductContents",
                column: "ContentId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
