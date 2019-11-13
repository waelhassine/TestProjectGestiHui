using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionHuil.Migrations
{
    public partial class UpdateTableAchat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mode",
                table: "Achats");

            migrationBuilder.DropColumn(
                name: "ModePaiement",
                table: "Achats");

            migrationBuilder.DropColumn(
                name: "MontantAchat",
                table: "Achats");

            migrationBuilder.DropColumn(
                name: "Prix",
                table: "Achats");

            migrationBuilder.RenameColumn(
                name: "ResteAPayer",
                table: "Achats",
                newName: "Prix_unitaire");

            migrationBuilder.AddColumn<int>(
                name: "TriturationId",
                table: "Achats",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Achats_TriturationId",
                table: "Achats",
                column: "TriturationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Achats_Triturations_TriturationId",
                table: "Achats",
                column: "TriturationId",
                principalTable: "Triturations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Achats_Triturations_TriturationId",
                table: "Achats");

            migrationBuilder.DropIndex(
                name: "IX_Achats_TriturationId",
                table: "Achats");

            migrationBuilder.DropColumn(
                name: "TriturationId",
                table: "Achats");

            migrationBuilder.RenameColumn(
                name: "Prix_unitaire",
                table: "Achats",
                newName: "ResteAPayer");

            migrationBuilder.AddColumn<string>(
                name: "Mode",
                table: "Achats",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModePaiement",
                table: "Achats",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MontantAchat",
                table: "Achats",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Prix",
                table: "Achats",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
