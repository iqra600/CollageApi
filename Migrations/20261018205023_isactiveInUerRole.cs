using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollageApi.Migrations
{
    /// <inheritdoc />
    public partial class isactiveInUerRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "UserRoleMap",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "UserRoleMap");
        }
    }
}
