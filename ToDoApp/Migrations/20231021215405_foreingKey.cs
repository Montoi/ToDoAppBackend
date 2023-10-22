using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApp.Migrations
{
    /// <inheritdoc />
    public partial class foreingKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Notes");

            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "Notes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Notes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ColorId", "Date" },
                values: new object[] { 1, new DateTime(2023, 10, 21, 17, 54, 5, 496, DateTimeKind.Local).AddTicks(5504) });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_ColorId",
                table: "Notes",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Colors_ColorId",
                table: "Notes",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Colors_ColorId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_ColorId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Notes");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Notes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Color", "Date" },
                values: new object[] { "#fff", new DateTime(2023, 10, 21, 16, 44, 8, 467, DateTimeKind.Local).AddTicks(7134) });
        }
    }
}
