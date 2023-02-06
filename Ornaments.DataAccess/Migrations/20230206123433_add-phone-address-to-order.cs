using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ornaments.DataAccess.Migrations
{
    public partial class addphoneaddresstoorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Orderd",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Orderd",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Orderd");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Orderd");
        }
    }
}
