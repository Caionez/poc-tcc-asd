using Microsoft.EntityFrameworkCore.Migrations;

namespace InfoCadastraisWebApp.Data.Migrations
{
    public partial class FormacaoPrestadores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Formacao",
                table: "Prestadores",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Formacao",
                table: "Prestadores");
        }
    }
}
