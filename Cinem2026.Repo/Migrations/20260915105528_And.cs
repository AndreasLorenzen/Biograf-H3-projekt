using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinema2026.Repo.Migrations
{
    /// <inheritdoc />
    public partial class And : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieHallId",
                table: "Persons",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MovieHalls",
                columns: table => new
                {
                    MovieHallId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieHalloccupied = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieHalls", x => x.MovieHallId);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Moviename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Movieage = table.Column<int>(type: "int", nullable: false),
                    MovieHallId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieId);
                    table.ForeignKey(
                        name: "FK_Movies_MovieHalls_MovieHallId",
                        column: x => x.MovieHallId,
                        principalTable: "MovieHalls",
                        principalColumn: "MovieHallId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_MovieHallId",
                table: "Persons",
                column: "MovieHallId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_MovieHallId",
                table: "Movies",
                column: "MovieHallId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_MovieHalls_MovieHallId",
                table: "Persons",
                column: "MovieHallId",
                principalTable: "MovieHalls",
                principalColumn: "MovieHallId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_MovieHalls_MovieHallId",
                table: "Persons");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "MovieHalls");

            migrationBuilder.DropIndex(
                name: "IX_Persons_MovieHallId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "MovieHallId",
                table: "Persons");
        }
    }
}
