using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CollageApi.Migrations
{
    /// <inheritdoc />
    public partial class addeddataIntoStudentForStartupPropject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "student",
                columns: new[] { "ID", "Age", "Email", "Name" },
                values: new object[,]
                {
                    { 1, 24, "iqra@gmail.com", "iqra" },
                    { 2, 25, "hira@gmail.com", "hira" },
                    { 3, 20, "hani@gmail.com", "hani" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "student",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "student",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "student",
                keyColumn: "ID",
                keyValue: 3);
        }
    }
}
