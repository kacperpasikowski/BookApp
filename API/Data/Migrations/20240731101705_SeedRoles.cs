using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
