using Microsoft.EntityFrameworkCore.Migrations;

namespace Diploma_Project.Entity_lib.Migrations
{
    public partial class initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Login_NumberPhone",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "NumberPhone",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Login",
                table: "Users",
                column: "Login",
                unique: true,
                filter: "[Login] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Login",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "NumberPhone",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Login_NumberPhone",
                table: "Users",
                columns: new[] { "Login", "NumberPhone" },
                unique: true,
                filter: "[Login] IS NOT NULL AND [NumberPhone] IS NOT NULL");
        }
    }
}
