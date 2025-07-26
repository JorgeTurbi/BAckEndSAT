using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackEndSAT.Migrations
{
    /// <inheritdoc />
    public partial class AddVacancies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriasVacante",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasVacante", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provincias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vacantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstitucionId = table.Column<int>(type: "int", nullable: false),
                    ProvinciaId = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TipoContrato = table.Column<int>(type: "int", nullable: false),
                    SalarioCompensacion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FechaLimiteAplicacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HorarioTrabajo = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    DuracionContrato = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    DescripcionPuesto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResponsabilidadesEspecificas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequisitosGenerales = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EducacionRequerida = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExperienciaRequerida = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HabilidadesCompetencias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeneficiosCompensaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InformacionContacto = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vacantes_CategoriasVacante_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "CategoriasVacante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vacantes_Instituciones_InstitucionId",
                        column: x => x.InstitucionId,
                        principalTable: "Instituciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vacantes_Provincias_ProvinciaId",
                        column: x => x.ProvinciaId,
                        principalTable: "Provincias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CategoriasVacante",
                columns: new[] { "Id", "Descripcion", "Nombre" },
                values: new object[,]
                {
                    { 1, null, "Operaciones Especiales" },
                    { 2, null, "Inteligencia y Contrainteligencia" },
                    { 3, null, "Seguridad Fronteriza" },
                    { 4, null, "Ciberseguridad" },
                    { 5, null, "Administración" },
                    { 6, null, "Logística" },
                    { 7, null, "Comunicaciones" }
                });

            migrationBuilder.InsertData(
                table: "Provincias",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Azua" },
                    { 2, "Baoruco" },
                    { 3, "Barahona" },
                    { 4, "Dajabón" },
                    { 5, "Duarte" },
                    { 6, "Elías Piña" },
                    { 7, "El Seibo" },
                    { 8, "Espaillat" },
                    { 9, "Hato Mayor" },
                    { 10, "Hermanas Mirabal" },
                    { 11, "Independencia" },
                    { 12, "La Altagracia" },
                    { 13, "La Romana" },
                    { 14, "La Vega" },
                    { 15, "María Trinidad Sánchez" },
                    { 16, "Monseñor Nouel" },
                    { 17, "Monte Cristi" },
                    { 18, "Monte Plata" },
                    { 19, "Pedernales" },
                    { 20, "Peravia" },
                    { 21, "Puerto Plata" },
                    { 22, "Samaná" },
                    { 23, "Sánchez Ramírez" },
                    { 24, "San Cristóbal" },
                    { 25, "San José de Ocoa" },
                    { 26, "San Juan" },
                    { 27, "San Pedro de Macorís" },
                    { 28, "Santiago" },
                    { 29, "Santiago Rodríguez" },
                    { 30, "Santo Domingo" },
                    { 31, "Valverde" },
                    { 32, "Distrito Nacional" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vacantes_CategoriaId",
                table: "Vacantes",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacantes_InstitucionId_CategoriaId_ProvinciaId",
                table: "Vacantes",
                columns: new[] { "InstitucionId", "CategoriaId", "ProvinciaId" });

            migrationBuilder.CreateIndex(
                name: "IX_Vacantes_IsActive",
                table: "Vacantes",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Vacantes_ProvinciaId",
                table: "Vacantes",
                column: "ProvinciaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vacantes");

            migrationBuilder.DropTable(
                name: "CategoriasVacante");

            migrationBuilder.DropTable(
                name: "Provincias");
        }
    }
}
