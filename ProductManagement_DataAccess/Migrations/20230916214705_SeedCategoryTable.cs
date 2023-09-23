using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 9, 16, 15, 47, 5, 88, DateTimeKind.Local).AddTicks(1377), 1, "Action" },
                    { 2, new DateTime(2023, 9, 16, 15, 47, 5, 88, DateTimeKind.Local).AddTicks(1379), 2, "Science" },
                    { 3, new DateTime(2023, 9, 16, 15, 47, 5, 88, DateTimeKind.Local).AddTicks(1381), 3, "History" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
