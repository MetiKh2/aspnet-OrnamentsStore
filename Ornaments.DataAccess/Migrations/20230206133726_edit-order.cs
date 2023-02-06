using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ornaments.DataAccess.Migrations
{
    public partial class editorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orderd_AspNetUsers_UserId",
                table: "Orderd");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orderd_OrderId",
                table: "OrderDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orderd",
                table: "Orderd");

            migrationBuilder.RenameTable(
                name: "Orderd",
                newName: "Orders");

            migrationBuilder.RenameIndex(
                name: "IX_Orderd_UserId",
                table: "Orders",
                newName: "IX_Orders_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Orderd");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "Orderd",
                newName: "IX_Orderd_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orderd",
                table: "Orderd",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orderd_AspNetUsers_UserId",
                table: "Orderd",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orderd_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "Orderd",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
