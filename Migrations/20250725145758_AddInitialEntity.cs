using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackEndSAT.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instituciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrlLogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instituciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstitutionId = table.Column<int>(type: "int", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Usuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cedula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Instituciones_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Instituciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "varchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departamentos",
                columns: new[] { "Id", "Direccion", "Director" },
                values: new object[,]
                {
                    { 1, "Dirección de Tecnología", "Capitan de Navío Jhonathan de la Cruz" },
                    { 2, "Recursos Humanos", "Mayor Piloto Jesica Heredia" },
                    { 3, "Operaciones", "Coronel de Infantería Juan Pérez" }
                });

            migrationBuilder.InsertData(
                table: "Instituciones",
                columns: new[] { "Id", "CodigoNombre", "Direccion", "Email", "Nombre", "Telefono", "UrlLogo" },
                values: new object[,]
                {
                    { 1, "CESAC", "Av. Aeropuerto, Santo Domingo", "info@cesac.mil.do", "Cuerpo Especializado en Seguridad Aeroportuaria y Aviación Civil", "809-999-1001", "/Logos/cesac.png" },
                    { 2, "CECCOMM", "Av. John F. Kennedy, Santo Domingo", "info@ceccomm.mil.do", "Cuerpo Especializado de Control de Combustibles", "809-999-1002", "/Logos/ceccomm.png" },
                    { 3, "CIUTRAN", "Zona Militar, Santo Domingo", "info@ciutran.mil.do", "Fuerza de Tarea Conjunta Ciudad Tranquila", "809-999-1003", "/Logos/ciutran.png" },
                    { 4, "UNADE", "Ciudad Militar, Santo Domingo", "info@unade.mil.do", "Universidad Nacional para la Defensa", "809-999-1004", "/Logos/unade.png" },
                    { 5, "ISSFFAA", "Calle Principal #10, Santo Domingo", "info@issffaa.mil.do", "Instituto de Seguridad Social de las Fuerzas Armadas", "809-999-1005", "/Logos/issffaa.png" },
                    { 6, "HIFA", "Av. Independencia, Santo Domingo", "info@hifa.mil.do", "Hospital de las Fuerzas Armadas", "809-999-1006", "/Logos/hifa.png" },
                    { 7, "EGDDHHyDIH", "Ciudad Militar, Santo Domingo", "info@egddhh.mil.do", "Escuela de Graduados de Derechos Humanos y Derecho Internacional Humanitario", "809-999-1007", "/Logos/egddhh.png" },
                    { 8, "FARD", "Base Aérea San Isidro, Santo Domingo Este", "info@fard.mil.do", "Fuerza Aérea de la República Dominicana", "809-999-1008", "/Logos/fard.png" },
                    { 9, "ERD", "Av. 27 de Febrero, Santo Domingo", "info@ejercito.mil.do", "Ejército de la República Dominicana", "809-999-1009", "/Logos/ejercito.png" },
                    { 10, "ARD", "Base Naval, Santo Domingo", "info@armada.mil.do", "Armada de la República Dominicana", "809-999-1010", "/Logos/armada.png" },
                    { 11, "MIDE", "Av. Luperón, Santo Domingo", "info@mide.mil.do", "Ministerio de Defensa", "809-999-1011", "/Logos/mide.png" },
                    { 12, "C5i", "Av. Luperón, Santo Domingo", "info@C5iffaa.gob.do", "C5i de las Fuerzas Armadas", "809-999-1011", "/Logos/mide.png" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_UserId_IsRevoked",
                table: "Sessions",
                columns: new[] { "UserId", "IsRevoked" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartamentoId",
                table: "Users",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_InstitutionId",
                table: "Users",
                column: "InstitutionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Departamentos");

            migrationBuilder.DropTable(
                name: "Instituciones");
        }
    }
}
