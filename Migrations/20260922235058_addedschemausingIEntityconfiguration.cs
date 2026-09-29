using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollageApi.Migrations
{
    /// <inheritdoc />
    public partial class addedschemausingIEntityconfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_student",
                table: "student");

            migrationBuilder.RenameTable(
                name: "student",
                newName: "Student");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student",
                table: "Student",
                column: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Student",
                table: "Student");

            migrationBuilder.RenameTable(
                name: "Student",
                newName: "student");

            migrationBuilder.AddPrimaryKey(
                name: "PK_student",
                table: "student",
                column: "ID");
        }
    }
}
