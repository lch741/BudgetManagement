using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddAppUserToCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "Transactions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Categorys",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Categorys_AppUserId",
                table: "Categorys",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categorys_AspNetUsers_AppUserId",
                table: "Categorys",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categorys_AspNetUsers_AppUserId",
                table: "Categorys");

            migrationBuilder.DropIndex(
                name: "IX_Categorys_AppUserId",
                table: "Categorys");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Categorys");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Transactions",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "TEXT");
        }
    }
}
