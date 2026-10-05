using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CollageApi.Migrations
{
    /// <inheritdoc />
    public partial class addedUserTypeAndSomeModification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleMaping_Roles_RoleId",
                table: "UserRoleMaping");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleMaping_Users_UserId",
                table: "UserRoleMaping");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoleMaping",
                table: "UserRoleMaping");

            migrationBuilder.DropIndex(
                name: "IX_UserRoleMaping_UserId",
                table: "UserRoleMaping");

            migrationBuilder.RenameTable(
                name: "UserRoleMaping",
                newName: "UserRoleMap");

            migrationBuilder.RenameColumn(
                name: "UserType",
                table: "Users",
                newName: "UserTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoleMaping_RoleId",
                table: "UserRoleMap",
                newName: "IX_UserRoleMap_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoleMap",
                table: "UserRoleMap",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "UserTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "UserTypes",
                columns: new[] { "ID", "Description", "UserTypeName" },
                values: new object[,]
                {
                    { 1, "for students", "Student" },
                    { 2, "for Teacher", "Teacher" },
                    { 3, "for HR", "HR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserTypeId",
                table: "Users",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "UK_UserRoleMapping",
                table: "UserRoleMap",
                columns: new[] { "UserId", "RoleId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleMap_Role",
                table: "UserRoleMap",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleMap_User",
                table: "UserRoleMap",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserType_User",
                table: "Users",
                column: "UserTypeId",
                principalTable: "UserTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleMap_Role",
                table: "UserRoleMap");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleMap_User",
                table: "UserRoleMap");

            migrationBuilder.DropForeignKey(
                name: "FK_UserType_User",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserTypes");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserTypeId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoleMap",
                table: "UserRoleMap");

            migrationBuilder.DropIndex(
                name: "UK_UserRoleMapping",
                table: "UserRoleMap");

            migrationBuilder.RenameTable(
                name: "UserRoleMap",
                newName: "UserRoleMaping");

            migrationBuilder.RenameColumn(
                name: "UserTypeId",
                table: "Users",
                newName: "UserType");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoleMap_RoleId",
                table: "UserRoleMaping",
                newName: "IX_UserRoleMaping_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoleMaping",
                table: "UserRoleMaping",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleMaping_UserId",
                table: "UserRoleMaping",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleMaping_Roles_RoleId",
                table: "UserRoleMaping",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleMaping_Users_UserId",
                table: "UserRoleMaping",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
