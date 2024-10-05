using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Route.IKEA.DAL.persistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGenderColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Geneder",
                table: "Employees",
                newName: "Gender");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Employees",
                newName: "Geneder");
        }
    }
}
