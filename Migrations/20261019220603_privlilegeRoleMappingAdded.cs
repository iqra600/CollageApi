using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollageApi.Migrations
{
    /// <inheritdoc />
    public partial class privlilegeRoleMappingAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RolePrivilegeMappings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PrivilegeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePrivilegeMappings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RolePrivilegeMappings_RolePrivileges_PrivilegeId",
                        column: x => x.PrivilegeId,
                        principalTable: "RolePrivileges",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePrivilegeMappings_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePrivilegeMappings_PrivilegeId",
                table: "RolePrivilegeMappings",
                column: "PrivilegeId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePrivilegeMappings_RoleId",
                table: "RolePrivilegeMappings",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePrivilegeMappings");
        }
    }
}
