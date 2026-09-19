using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema2026.Repo.Migrations
{
    /// <inheritdoc />
    public partial class RettetPersonOgMovieHallRelationer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_MovieHalls_MovieHallId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_MovieHallId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "MovieHallId",
                table: "Movies");

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "MovieHalls",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieHalls_MovieId",
                table: "MovieHalls",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieHalls_Movies_MovieId",
                table: "MovieHalls",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieHalls_Movies_MovieId",
                table: "MovieHalls");

            migrationBuilder.DropIndex(
                name: "IX_MovieHalls_MovieId",
                table: "MovieHalls");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "MovieHalls");

            migrationBuilder.AddColumn<int>(
                name: "MovieHallId",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_MovieHallId",
                table: "Movies",
                column: "MovieHallId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_MovieHalls_MovieHallId",
                table: "Movies",
                column: "MovieHallId",
                principalTable: "MovieHalls",
                principalColumn: "MovieHallId");
        }
    }
}
