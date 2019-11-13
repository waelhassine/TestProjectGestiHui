using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionHuil.Migrations
{
    public partial class AddGrogonsToFacture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GrignonId",
                table: "Factures",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Factures_GrignonId",
                table: "Factures",
                column: "GrignonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Factures_Grignons_GrignonId",
                table: "Factures",
                column: "GrignonId",
                principalTable: "Grignons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factures_Grignons_GrignonId",
                table: "Factures");

            migrationBuilder.DropIndex(
                name: "IX_Factures_GrignonId",
                table: "Factures");

            migrationBuilder.DropColumn(
                name: "GrignonId",
                table: "Factures");
        }
    }
}
