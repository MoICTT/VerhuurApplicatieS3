using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VerhuurApplicatieAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordHashToKlant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Klanten",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Klanten");
        }
    }
}
