using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DB_Operation_With_EfCoreApp.Migrations
{
    /// <inheritdoc />
    public partial class addedOneCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CurrencyTypes",
                columns: new[] { "Id", "Currency", "Description" },
                values: new object[,]
                {
                    { 4, "INR", "Indian Rupees" },
                    { 5, "INR", "From India" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CurrencyTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CurrencyTypes",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
