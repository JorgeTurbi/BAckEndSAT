using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndSAT.Migrations
{
    /// <inheritdoc />
    public partial class CampoFechaEntrevista : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Fechaentrevista",
                table: "AplicacionVacantes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 30, 23, 26, 1, 552, DateTimeKind.Utc).AddTicks(4106));

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 30, 23, 26, 1, 552, DateTimeKind.Utc).AddTicks(4110));

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 30, 23, 26, 1, 552, DateTimeKind.Utc).AddTicks(4112));

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 30, 23, 26, 1, 552, DateTimeKind.Utc).AddTicks(4113));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fechaentrevista",
                table: "AplicacionVacantes");

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
    }
}
