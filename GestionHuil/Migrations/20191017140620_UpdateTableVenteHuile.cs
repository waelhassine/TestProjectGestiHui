using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionHuil.Migrations
{
    public partial class UpdateTableVenteHuile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VarieteId",
                table: "VenteHuiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VenteHuiles_VarieteId",
                table: "VenteHuiles",
                column: "VarieteId");

            migrationBuilder.AddForeignKey(
                name: "FK_VenteHuiles_Varietes_VarieteId",
                table: "VenteHuiles",
                column: "VarieteId",
                principalTable: "Varietes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VenteHuiles_Varietes_VarieteId",
                table: "VenteHuiles");

            migrationBuilder.DropIndex(
                name: "IX_VenteHuiles_VarieteId",
                table: "VenteHuiles");

            migrationBuilder.DropColumn(
                name: "VarieteId",
                table: "VenteHuiles");
        }
    }
}
