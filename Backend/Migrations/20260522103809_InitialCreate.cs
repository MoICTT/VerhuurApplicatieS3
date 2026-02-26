using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VerhuurApplicatieAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Merk = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bouwjaar = table.Column<int>(type: "int", nullable: false),
                    Brandstof = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AantalZitplaatsen = table.Column<int>(type: "int", nullable: false),
                    Kenteken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrijsPerDag = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Beschikbaar = table.Column<bool>(type: "bit", nullable: false),
                    AfbeeldingUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Klanten",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Voornaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Achternaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klanten", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservaties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EindDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AutoId = table.Column<int>(type: "int", nullable: false),
                    KlantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservaties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservaties_Autos_AutoId",
                        column: x => x.AutoId,
                        principalTable: "Autos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservaties_Klanten_KlantId",
                        column: x => x.KlantId,
                        principalTable: "Klanten",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Autos",
                columns: new[] { "Id", "AantalZitplaatsen", "AfbeeldingUrl", "Beschikbaar", "Bouwjaar", "Brandstof", "Kenteken", "Merk", "Model", "PrijsPerDag" },
                values: new object[,]
                {
                    { 1, 5, null, true, 2020, "Benzine", "AB-123-C", "Toyota", "Corolla", 45.00m },
                    { 2, 5, null, true, 2021, "Benzine", "DE-456-F", "Volkswagen", "Golf", 55.00m },
                    { 3, 5, null, true, 2023, "Elektrisch", "GH-789-I", "Tesla", "Model 3", 85.00m },
                    { 4, 5, null, false, 2019, "Diesel", "JK-012-L", "Ford", "Focus", 40.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservaties_AutoId",
                table: "Reservaties",
                column: "AutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservaties_KlantId",
                table: "Reservaties",
                column: "KlantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservaties");

            migrationBuilder.DropTable(
                name: "Autos");

            migrationBuilder.DropTable(
                name: "Klanten");
        }
    }
}
