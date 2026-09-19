using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema2026.Repo.Migrations
{
    /// <inheritdoc />
    public partial class Andreas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Persons",
                newName: "Personname");

            migrationBuilder.RenameColumn(
                name: "age",
                table: "Persons",
                newName: "Personage");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Persons",
                newName: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Personname",
                table: "Persons",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Personage",
                table: "Persons",
                newName: "age");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Persons",
                newName: "Id");
        }
    }
}
