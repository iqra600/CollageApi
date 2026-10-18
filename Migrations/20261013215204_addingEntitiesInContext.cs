using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollageApi.Migrations
{
    /// <inheritdoc />
    public partial class addingEntitiesInContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "Description", "UserTypeName" },
                values: new object[] { "for Admin", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "Description", "UserTypeName" },
                values: new object[] { "for HR", "HR" });
        }
    }
}
