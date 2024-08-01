using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2fcf09ea-231d-42bc-b8d1-94e725421c34"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("300ada41-66f4-40af-9fb0-032a767ad95b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b94f5888-f2bc-4462-943e-b333f1cb4007"));

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateRead",
                table: "UserBooks",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("33050927-a2cb-43b3-b395-8e604663aa57"), null, "Admin", "ADMIN" },
                    { new Guid("52fdf143-417a-45c5-9333-c798eb110ac5"), null, "User", "USER" },
                    { new Guid("5850c2ed-fc73-4901-89e7-8b62fd58c1ca"), null, "Moderator", "MODERATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("33050927-a2cb-43b3-b395-8e604663aa57"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("52fdf143-417a-45c5-9333-c798eb110ac5"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5850c2ed-fc73-4901-89e7-8b62fd58c1ca"));

            migrationBuilder.DropColumn(
                name: "DateRead",
                table: "UserBooks");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2fcf09ea-231d-42bc-b8d1-94e725421c34"), null, "Admin", "ADMIN" },
                    { new Guid("300ada41-66f4-40af-9fb0-032a767ad95b"), null, "Moderator", "MODERATOR" },
                    { new Guid("b94f5888-f2bc-4462-943e-b333f1cb4007"), null, "User", "USER" }
                });
        }
    }
}
