using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmesAPI.Migrations
{
    public partial class MovieTheaterManagerRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "MovieTheaters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MovieTheaters_ManagerId",
                table: "MovieTheaters",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieTheaters_Managers_ManagerId",
                table: "MovieTheaters",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieTheaters_Managers_ManagerId",
                table: "MovieTheaters");

            migrationBuilder.DropIndex(
                name: "IX_MovieTheaters_ManagerId",
                table: "MovieTheaters");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "MovieTheaters");
        }
    }
}
