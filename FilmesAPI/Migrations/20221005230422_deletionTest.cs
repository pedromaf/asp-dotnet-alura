using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmesAPI.Migrations
{
    public partial class deletionTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieTheaters_Managers_ManagerId",
                table: "MovieTheaters");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieTheaters_Managers_ManagerId",
                table: "MovieTheaters",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieTheaters_Managers_ManagerId",
                table: "MovieTheaters");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieTheaters_Managers_ManagerId",
                table: "MovieTheaters",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
