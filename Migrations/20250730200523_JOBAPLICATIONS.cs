using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackEndSAT.Migrations
{
    /// <inheritdoc />
    public partial class JOBAPLICATIONS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AplicacionVacantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AplicanteId = table.Column<int>(type: "int", nullable: false),
                    VacanteId = table.Column<int>(type: "int", nullable: false),
                    FechaAplicacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoId = table.Column<int>(type: "int", nullable: false),
                    MatchPorcentaje = table.Column<double>(type: "float", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AplicacionVacantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AplicacionVacantes_Aplicantes_AplicanteId",
                        column: x => x.AplicanteId,
                        principalTable: "Aplicantes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AplicacionVacantes_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AplicacionVacantes_Vacantes_VacanteId",
                        column: x => x.VacanteId,
                        principalTable: "Vacantes",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "Id", "CreatedAt", "Descripcion", "IsActive", "Nombre" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 30, 20, 5, 23, 8, DateTimeKind.Utc).AddTicks(1132), "Aplicación recibida, en espera de revisión", true, "Pendiente" },
                    { 2, new DateTime(2025, 7, 30, 20, 5, 23, 8, DateTimeKind.Utc).AddTicks(1143), "Aplicación está siendo evaluada por el reclutador", true, "En revisión" },
                    { 3, new DateTime(2025, 7, 30, 20, 5, 23, 8, DateTimeKind.Utc).AddTicks(1144), "Aplicante seleccionado para la vacante", true, "Aprobado" },
                    { 4, new DateTime(2025, 7, 30, 20, 5, 23, 8, DateTimeKind.Utc).AddTicks(1146), "Aplicante no seleccionado para esta vacante", true, "Rechazado" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AplicacionVacantes_AplicanteId",
                table: "AplicacionVacantes",
                column: "AplicanteId");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacionVacantes_EstadoId",
                table: "AplicacionVacantes",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_AplicacionVacantes_VacanteId",
                table: "AplicacionVacantes",
                column: "VacanteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AplicacionVacantes");

            migrationBuilder.DropTable(
                name: "Estados");
        }
    }
}
