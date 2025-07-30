using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackEndSAT.Migrations
{
    /// <inheritdoc />
    public partial class Initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstitucionMilitar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Institucion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstitucionMilitar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rangos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstitucionMilitarId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rangos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rangos_InstitucionMilitar_InstitucionMilitarId",
                        column: x => x.InstitucionMilitarId,
                        principalTable: "InstitucionMilitar",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "InstitucionMilitar",
                columns: new[] { "Id", "Institucion" },
                values: new object[,]
                {
                    { 1, "Ejército (ERD)" },
                    { 2, "Armada (ARD)" },
                    { 3, "Fuerza Aérea (FARD)" },
                    { 4, "Ministerio de Defensa (MIDE)" }
                });

            migrationBuilder.InsertData(
                table: "Rangos",
                columns: new[] { "Id", "InstitucionMilitarId", "Nombre" },
                values: new object[,]
                {
                    { 1, 1, "Teniente General" },
                    { 2, 1, "Mayor General" },
                    { 3, 1, "General de Brigada" },
                    { 4, 1, "Coronel" },
                    { 5, 1, "Teniente Coronel" },
                    { 6, 1, "Mayor" },
                    { 7, 1, "Capitán" },
                    { 8, 1, "Primer Teniente" },
                    { 9, 1, "Segundo Teniente" },
                    { 10, 1, "Sargento Mayor" },
                    { 11, 1, "Sargento" },
                    { 12, 1, "Cabo" },
                    { 13, 1, "Raso" },
                    { 14, 1, "Asimilado Militar" },
                    { 15, 2, "Almirante" },
                    { 16, 2, "Vicealmirante" },
                    { 17, 2, "Contralmirante" },
                    { 18, 2, "Capitán de Navio" },
                    { 19, 2, "Capitán de Fragata" },
                    { 20, 2, "Capitan de Corbeta" },
                    { 21, 2, "Teniente de Navio" },
                    { 22, 2, "Teniente de Fragata" },
                    { 23, 2, "Teniente de Corbeta" },
                    { 24, 2, "Sargento Mayor" },
                    { 25, 2, "Sargento" },
                    { 26, 2, "Cabo" },
                    { 27, 2, "Raso" },
                    { 28, 2, "Asimilado Militar" },
                    { 29, 2, "Auxiliar" },
                    { 30, 3, "Teniente General Piloto" },
                    { 31, 3, "Mayor General Piloto" },
                    { 32, 3, "General de Brigada Piloto" },
                    { 33, 3, "Coronel" },
                    { 34, 3, "Teniente Coronel" },
                    { 35, 3, "Mayor" },
                    { 36, 3, "Capitan" },
                    { 37, 3, "Primer Teniente" },
                    { 38, 3, "Segundo Teniente" },
                    { 39, 3, "Sargento Mayor" },
                    { 40, 3, "Sargento" },
                    { 41, 3, "Cabo" },
                    { 42, 3, "Raso" },
                    { 43, 3, "Asimilado Militar" },
                    { 44, 4, "Asimilado Militar" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rangos_InstitucionMilitarId",
                table: "Rangos",
                column: "InstitucionMilitarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rangos");

            migrationBuilder.DropTable(
                name: "InstitucionMilitar");
        }
    }
}
