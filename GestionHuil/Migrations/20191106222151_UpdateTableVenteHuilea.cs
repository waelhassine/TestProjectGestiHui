using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionHuil.Migrations
{
    public partial class UpdateTableVenteHuilea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Prix_bidon",
                table: "VenteHuiles",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "Qte_bidon",
                table: "VenteHuiles",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prix_bidon",
                table: "VenteHuiles");

            migrationBuilder.DropColumn(
                name: "Qte_bidon",
                table: "VenteHuiles");
        }
    }
}
