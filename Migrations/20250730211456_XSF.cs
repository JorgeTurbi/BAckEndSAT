using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndSAT.Migrations
{
    /// <inheritdoc />
    public partial class XSF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 30, 21, 14, 55, 166, DateTimeKind.Utc).AddTicks(784));

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 30, 21, 14, 55, 166, DateTimeKind.Utc).AddTicks(791));

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 30, 21, 14, 55, 166, DateTimeKind.Utc).AddTicks(793));

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 30, 21, 14, 55, 166, DateTimeKind.Utc).AddTicks(795));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 30, 20, 5, 23, 8, DateTimeKind.Utc).AddTicks(1132));

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 30, 20, 5, 23, 8, DateTimeKind.Utc).AddTicks(1143));

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 30, 20, 5, 23, 8, DateTimeKind.Utc).AddTicks(1144));

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 30, 20, 5, 23, 8, DateTimeKind.Utc).AddTicks(1146));
        }
    }
}
