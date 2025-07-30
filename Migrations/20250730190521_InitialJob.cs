using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndSAT.Migrations
{
    /// <inheritdoc />
    public partial class InitialJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Educations",
                newName: "CertificatePdf");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CertificatePdf",
                table: "Educations",
                newName: "Description");
        }
    }
}
