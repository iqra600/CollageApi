using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CollageApi.Migrations
{
    /// <inheritdoc />
    public partial class DepartmentAdeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentID",
                table: "Student",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "ID", "DepartmentName", "Description" },
                values: new object[,]
                {
                    { 1, "IT", "IT Department" },
                    { 2, "CS", "IT CS" }
                });

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "ID",
                keyValue: 1,
                column: "DepartmentID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "ID",
                keyValue: 2,
                column: "DepartmentID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Student",
                keyColumn: "ID",
                keyValue: 3,
                column: "DepartmentID",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Student_DepartmentID",
                table: "Student",
                column: "DepartmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Department_DepartmentID",
                table: "Student",
                column: "DepartmentID",
                principalTable: "Department",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Department_DepartmentID",
                table: "Student");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Student_DepartmentID",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "DepartmentID",
                table: "Student");
        }
    }
}
