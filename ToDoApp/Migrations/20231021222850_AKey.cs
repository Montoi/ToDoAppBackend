using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApp.Migrations
{
    /// <inheritdoc />
    public partial class AKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Notes",
                keyColumn: "Id",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "Id", "ColorId", "Date", "NoteText", "Priority", "UserId" },
                values: new object[] { 1, 1, new DateTime(2023, 10, 21, 18, 21, 51, 395, DateTimeKind.Local).AddTicks(3218), "Gracias por visitar esta aplicación", "Alta", 2 });
        }
    }
}
