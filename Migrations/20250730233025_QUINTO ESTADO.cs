using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndSAT.Migrations
{
    /// <inheritdoc />
    public partial class QUINTOESTADO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 30, 23, 30, 24, 790, DateTimeKind.Utc).AddTicks(1773));

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 30, 23, 30, 24, 790, DateTimeKind.Utc).AddTicks(1781));

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 30, 23, 30, 24, 790, DateTimeKind.Utc).AddTicks(1783));

            migrationBuilder.UpdateData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 7, 30, 23, 30, 24, 790, DateTimeKind.Utc).AddTicks(1784));

            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "Id", "CreatedAt", "Descripcion", "IsActive", "Nombre" },
                values: new object[] { 5, new DateTime(2025, 7, 30, 23, 30, 24, 790, DateTimeKind.Utc).AddTicks(1785), "Se ha coordinado una entrevista con el aplicante", true, "Entrevista programada" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Estados",
                keyColumn: "Id",
                keyValue: 5);

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
    }
}
