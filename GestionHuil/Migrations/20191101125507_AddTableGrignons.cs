using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionHuil.Migrations
{
    public partial class AddTableGrignons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grignons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Poids = table.Column<int>(nullable: false),
                    Prix_unitaire = table.Column<float>(nullable: false),
                    MontantAchat = table.Column<float>(nullable: false),
                    Vehicule = table.Column<string>(nullable: true),
                    Matricule = table.Column<string>(nullable: true),
                    Transporteur = table.Column<string>(nullable: true),
                    Chaufeur = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grignons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grignons_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grignons_ClientId",
                table: "Grignons",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grignons");
        }
    }
}
