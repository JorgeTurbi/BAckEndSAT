using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndSAT.Migrations
{
    /// <inheritdoc />
    public partial class UserIdadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Users_UserId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departamentos_DepartamentoId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Instituciones_InstitutionId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacantes_Instituciones_InstitucionId",
                table: "Vacantes");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Vacantes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vacantes_UserId",
                table: "Vacantes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Users_UserId",
                table: "Sessions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departamentos_DepartamentoId",
                table: "Users",
                column: "DepartamentoId",
                principalTable: "Departamentos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Instituciones_InstitutionId",
                table: "Users",
                column: "InstitutionId",
                principalTable: "Instituciones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacantes_Instituciones_InstitucionId",
                table: "Vacantes",
                column: "InstitucionId",
                principalTable: "Instituciones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacantes_Users_UserId",
                table: "Vacantes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Users_UserId",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departamentos_DepartamentoId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Instituciones_InstitutionId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacantes_Instituciones_InstitucionId",
                table: "Vacantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacantes_Users_UserId",
                table: "Vacantes");

            migrationBuilder.DropIndex(
                name: "IX_Vacantes_UserId",
                table: "Vacantes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Vacantes");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Users_UserId",
                table: "Sessions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departamentos_DepartamentoId",
                table: "Users",
                column: "DepartamentoId",
                principalTable: "Departamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Instituciones_InstitutionId",
                table: "Users",
                column: "InstitutionId",
                principalTable: "Instituciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacantes_Instituciones_InstitucionId",
                table: "Vacantes",
                column: "InstitucionId",
                principalTable: "Instituciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
