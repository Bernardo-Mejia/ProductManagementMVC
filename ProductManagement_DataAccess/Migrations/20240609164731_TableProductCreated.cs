using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductManagement.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TableProductCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price50 = table.Column<double>(type: "float", nullable: false),
                    Price100 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 9, 10, 47, 30, 945, DateTimeKind.Local).AddTicks(3663));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 9, 10, 47, 30, 945, DateTimeKind.Local).AddTicks(3667));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 6, 9, 10, 47, 30, 945, DateTimeKind.Local).AddTicks(3669));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "Description", "ISBN", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { 1, "Humberto Eco", "Valiéndose de las características propias de la novela gótica; la crónica medieval y la novela policíaca; el nombre de la rosa narra las actividades detectivescas de Guillermo de Baskerville; quien busca esclarecer los crímenes cometidos en una abadía benedictina en el año 1327.", "978-6073103008", 55.0, 50.0, 35.0, 50.0, "Nombre de la Rosa" },
                    { 2, "Gabriel García Márquez", "Muchos años después, frente al pelotón de fusilamiento, el coronel Aureliano Buendía había de recordar aquella tarde remota en que su padre lo llevó a conocer el hielo.", "978-6070728792", 40.0, 30.0, 20.0, 25.0, "Cien años de soledad" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 9, 16, 15, 47, 5, 88, DateTimeKind.Local).AddTicks(1377));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2023, 9, 16, 15, 47, 5, 88, DateTimeKind.Local).AddTicks(1379));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2023, 9, 16, 15, 47, 5, 88, DateTimeKind.Local).AddTicks(1381));
        }
    }
}
