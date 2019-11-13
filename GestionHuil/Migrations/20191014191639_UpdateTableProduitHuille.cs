using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionHuil.Migrations
{
    public partial class UpdateTableProduitHuille : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prix_unitaire",
                table: "ProduitHuilles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Prix_unitaire",
                table: "ProduitHuilles",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
