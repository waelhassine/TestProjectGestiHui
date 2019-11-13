using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionHuil.Migrations
{
    public partial class UpdateTableTrituration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Triturations_Clients_ClientId",
                table: "Triturations");

            migrationBuilder.DropForeignKey(
                name: "FK_Triturations_Varietes_VarieteId",
                table: "Triturations");

            migrationBuilder.AlterColumn<int>(
                name: "VarieteId",
                table: "Triturations",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Triturations",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Triturations_Clients_ClientId",
                table: "Triturations",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Triturations_Varietes_VarieteId",
                table: "Triturations",
                column: "VarieteId",
                principalTable: "Varietes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Triturations_Clients_ClientId",
                table: "Triturations");

            migrationBuilder.DropForeignKey(
                name: "FK_Triturations_Varietes_VarieteId",
                table: "Triturations");

            migrationBuilder.AlterColumn<int>(
                name: "VarieteId",
                table: "Triturations",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Triturations",
                nullable: true,
                oldClrType: typeof(int));

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
    }
}
