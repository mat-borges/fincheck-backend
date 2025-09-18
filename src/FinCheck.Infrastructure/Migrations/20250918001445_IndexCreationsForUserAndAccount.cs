using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinCheck.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IndexCreationsForUserAndAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_UserId",
                schema: "fdbo",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "fdbo",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "fdbo",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId_Name",
                schema: "fdbo",
                table: "Accounts",
                columns: new[] { "UserId", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                schema: "fdbo",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_UserId_Name",
                schema: "fdbo",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "fdbo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                schema: "fdbo",
                table: "Accounts",
                column: "UserId");
        }
    }
}
