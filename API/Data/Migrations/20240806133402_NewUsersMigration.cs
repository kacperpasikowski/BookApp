using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewUsersMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2f70c680-b20e-4233-bf0f-a285933b28a1"), null, "Admin", "ADMIN" },
                    { new Guid("7e7b2f9d-6c42-4aa1-ad1e-4650f6f68dc5"), null, "User", "USER" },
                    { new Guid("8c39aee0-16d4-4922-9fc7-6aae40b9380f"), null, "Moderator", "MODERATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2f70c680-b20e-4233-bf0f-a285933b28a1"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7e7b2f9d-6c42-4aa1-ad1e-4650f6f68dc5"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8c39aee0-16d4-4922-9fc7-6aae40b9380f"));
        }
    }
}
