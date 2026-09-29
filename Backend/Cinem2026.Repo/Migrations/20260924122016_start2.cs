using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema2026.Repo.Migrations
{
    /// <inheritdoc />
    public partial class start2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "Moviename",
                table: "Movies",
                newName: "movieName");

            migrationBuilder.RenameColumn(
                name: "Movieage",
                table: "Movies",
                newName: "movieDuration");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Admins",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Admins",
                newName: "password");

            migrationBuilder.AddColumn<int>(
                name: "adminlevel",
                table: "Admins",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "adminlevel",
                table: "Admins");

            migrationBuilder.RenameColumn(
                name: "movieName",
                table: "Movies",
                newName: "Moviename");

            migrationBuilder.RenameColumn(
                name: "movieDuration",
                table: "Movies",
                newName: "Movieage");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Admins",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Admins",
                newName: "Password");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
