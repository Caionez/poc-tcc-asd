using Microsoft.EntityFrameworkCore.Migrations;

namespace InfoCadastraisWebApp.Data.Migrations
{
    public partial class Conveniados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consulta_Conveniado_ConveniadoId",
                table: "Consulta");

            migrationBuilder.DropForeignKey(
                name: "FK_Prestadores_Conveniado_ConveniadoId",
                table: "Prestadores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conveniado",
                table: "Conveniado");

            migrationBuilder.RenameTable(
                name: "Conveniado",
                newName: "Conveniados");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conveniados",
                table: "Conveniados",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Consulta_Conveniados_ConveniadoId",
                table: "Consulta",
                column: "ConveniadoId",
                principalTable: "Conveniados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prestadores_Conveniados_ConveniadoId",
                table: "Prestadores",
                column: "ConveniadoId",
                principalTable: "Conveniados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consulta_Conveniados_ConveniadoId",
                table: "Consulta");

            migrationBuilder.DropForeignKey(
                name: "FK_Prestadores_Conveniados_ConveniadoId",
                table: "Prestadores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conveniados",
                table: "Conveniados");

            migrationBuilder.RenameTable(
                name: "Conveniados",
                newName: "Conveniado");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conveniado",
                table: "Conveniado",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Consulta_Conveniado_ConveniadoId",
                table: "Consulta",
                column: "ConveniadoId",
                principalTable: "Conveniado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prestadores_Conveniado_ConveniadoId",
                table: "Prestadores",
                column: "ConveniadoId",
                principalTable: "Conveniado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
