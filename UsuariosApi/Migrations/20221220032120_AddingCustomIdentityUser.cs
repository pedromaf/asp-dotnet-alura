using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsuariosAPI.Migrations
{
    public partial class AddingCustomIdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99998,
                column: "ConcurrencyStamp",
                value: "f61040ec-cfed-4881-a045-0beb8323be4c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "f4c7209c-4800-4b0f-b4cd-38b78c5cf972");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "524d9191-e993-468c-8b4e-9085b00cdda1", "AQAAAAEAACcQAAAAEFqBD6WbYU31StYUPlAMRz3uTYg6tdpgBAUQlVn6eUkMU9TbUvdXJQEfq5Bcf5a1ZQ==", "b8fdb877-12b3-4d87-b849-bef950bbcfb3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99998,
                column: "ConcurrencyStamp",
                value: "e17b30a8-2faf-4c74-9bcf-71fbc53d1f54");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "3929b7ba-1187-4d2e-ad7b-08b1c37e3157");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e26cfd6-b8fd-4999-b1b1-5b6091d63c8c", "AQAAAAEAACcQAAAAEAc/xgsqYqut3NlRVZb3DqNUmHV20xEEDLwWafivulcvGLyCg8ZAjcmlrdZglTUjLA==", "894d53fb-d55a-4391-869c-aac8b30ebe4e" });
        }
    }
}
