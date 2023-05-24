using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobileStore.Infrastructure.Migrations
{
    public partial class AddImgPropertyInPloneModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "Phones",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "Phones");
        }
    }
}
