using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionHuil.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Achats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QteAchete = table.Column<int>(nullable: false),
                    Prix = table.Column<float>(nullable: false),
                    MontantAchat = table.Column<float>(nullable: false),
                    ModePaiement = table.Column<string>(nullable: true),
                    ResteAPayer = table.Column<float>(nullable: false),
                    Mode = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nom = table.Column<string>(nullable: true),
                    Tel = table.Column<string>(nullable: true),
                    Gsm = table.Column<string>(nullable: true),
                    Ville = table.Column<string>(nullable: true),
                    Solde = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NomPrenom = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Tel = table.Column<string>(nullable: true),
                    Photo = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Fonction = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Triturations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    PrixUnitaire = table.Column<float>(nullable: false),
                    Montant = table.Column<float>(nullable: false),
                    HuileObtenu = table.Column<int>(nullable: false),
                    QteLivree = table.Column<int>(nullable: false),
                    HuileRestante = table.Column<int>(nullable: false),
                    Poids = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Triturations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Varietes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Varietes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockageOlives",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Poids = table.Column<int>(nullable: false),
                    Chauffeur = table.Column<string>(nullable: true),
                    Vehicule = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    VarieteId = table.Column<int>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    TriturationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockageOlives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockageOlives_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockageOlives_Triturations_TriturationId",
                        column: x => x.TriturationId,
                        principalTable: "Triturations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockageOlives_Varietes_VarieteId",
                        column: x => x.VarieteId,
                        principalTable: "Varietes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockageOlives_ClientId",
                table: "StockageOlives",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_StockageOlives_TriturationId",
                table: "StockageOlives",
                column: "TriturationId");

            migrationBuilder.CreateIndex(
                name: "IX_StockageOlives_VarieteId",
                table: "StockageOlives",
                column: "VarieteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achats");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "StockageOlives");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Triturations");

            migrationBuilder.DropTable(
                name: "Varietes");
        }
    }
}
