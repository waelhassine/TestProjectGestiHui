using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionHuil.Migrations
{
    public partial class updateTabeTrituration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Triturations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VarieteId",
                table: "Triturations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Triturations_ClientId",
                table: "Triturations",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Triturations_VarieteId",
                table: "Triturations",
                column: "VarieteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Triturations_Clients_ClientId",
                table: "Triturations",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Triturations_Varietes_VarieteId",
                table: "Triturations",
                column: "VarieteId",
                principalTable: "Varietes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Triturations_Clients_ClientId",
                table: "Triturations");

            migrationBuilder.DropForeignKey(
                name: "FK_Triturations_Varietes_VarieteId",
                table: "Triturations");

            migrationBuilder.DropIndex(
                name: "IX_Triturations_ClientId",
                table: "Triturations");

            migrationBuilder.DropIndex(
                name: "IX_Triturations_VarieteId",
                table: "Triturations");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Triturations");

            migrationBuilder.DropColumn(
                name: "VarieteId",
                table: "Triturations");
        }
    }
}
