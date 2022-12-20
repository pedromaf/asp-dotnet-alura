using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsuariosAPI.Migrations
{
    public partial class regularUserRoleCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "3929b7ba-1187-4d2e-ad7b-08b1c37e3157");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 99998, "e17b30a8-2faf-4c74-9bcf-71fbc53d1f54", "regular-user", "REGULAR-USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e26cfd6-b8fd-4999-b1b1-5b6091d63c8c", "AQAAAAEAACcQAAAAEAc/xgsqYqut3NlRVZb3DqNUmHV20xEEDLwWafivulcvGLyCg8ZAjcmlrdZglTUjLA==", "894d53fb-d55a-4391-869c-aac8b30ebe4e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99998);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "e7d05b27-f5ca-4d0b-8f5d-f33529764522");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e6b06b79-2921-46fe-a18f-233c28a82895", "AQAAAAEAACcQAAAAENP3Z6RhbVhEgUDiubOBiZHknYFsMP3f2foPFTgf6fj5fJmjObDhZOc12TPs05SDeg==", "95cf75b6-18fe-4399-9dcd-b8fc10dd31ce" });
        }
    }
}
