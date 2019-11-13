using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionHuil.Migrations
{
    public partial class AddTableFacture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TypeFactures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nom = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeFactures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Factures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Montant = table.Column<float>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    TypeFactureId = table.Column<int>(nullable: false),
                    TriturationId = table.Column<int>(nullable: true),
                    AchatId = table.Column<int>(nullable: true),
                    VenteHuileId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Factures_Achats_AchatId",
                        column: x => x.AchatId,
                        principalTable: "Achats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Factures_Triturations_TriturationId",
                        column: x => x.TriturationId,
                        principalTable: "Triturations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Factures_TypeFactures_TypeFactureId",
                        column: x => x.TypeFactureId,
                        principalTable: "TypeFactures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Factures_VenteHuiles_VenteHuileId",
                        column: x => x.VenteHuileId,
                        principalTable: "VenteHuiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Factures_AchatId",
                table: "Factures",
                column: "AchatId");

            migrationBuilder.CreateIndex(
                name: "IX_Factures_TriturationId",
                table: "Factures",
                column: "TriturationId");

            migrationBuilder.CreateIndex(
                name: "IX_Factures_TypeFactureId",
                table: "Factures",
                column: "TypeFactureId");

            migrationBuilder.CreateIndex(
                name: "IX_Factures_VenteHuileId",
                table: "Factures",
                column: "VenteHuileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Factures");

            migrationBuilder.DropTable(
                name: "TypeFactures");
        }
    }
}
