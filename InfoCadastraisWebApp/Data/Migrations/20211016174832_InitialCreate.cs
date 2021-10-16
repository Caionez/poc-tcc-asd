using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InfoCadastraisWebApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Associados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Associados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conveniado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Endereco = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conveniado", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Especialidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanoAssociado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataVigencia = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TipoPlano = table.Column<int>(type: "INTEGER", nullable: false),
                    PlanoEmpresarial = table.Column<bool>(type: "INTEGER", nullable: false),
                    PlanoOdontologico = table.Column<bool>(type: "INTEGER", nullable: false),
                    AssociadoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanoAssociado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanoAssociado_Associados_AssociadoId",
                        column: x => x.AssociadoId,
                        principalTable: "Associados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prestadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    ConveniadoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prestadores_Conveniado_ConveniadoId",
                        column: x => x.ConveniadoId,
                        principalTable: "Conveniado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Consulta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataConsulta = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PlanoAssociadoId = table.Column<int>(type: "INTEGER", nullable: true),
                    PrestadorId = table.Column<int>(type: "INTEGER", nullable: true),
                    EspecialidadeId = table.Column<int>(type: "INTEGER", nullable: true),
                    ConveniadoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consulta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consulta_Conveniado_ConveniadoId",
                        column: x => x.ConveniadoId,
                        principalTable: "Conveniado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Consulta_Especialidades_EspecialidadeId",
                        column: x => x.EspecialidadeId,
                        principalTable: "Especialidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Consulta_PlanoAssociado_PlanoAssociadoId",
                        column: x => x.PlanoAssociadoId,
                        principalTable: "PlanoAssociado",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Consulta_Prestadores_PrestadorId",
                        column: x => x.PrestadorId,
                        principalTable: "Prestadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EspecialidadePrestador",
                columns: table => new
                {
                    EspecialidadesId = table.Column<int>(type: "INTEGER", nullable: false),
                    PrestadoresId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EspecialidadePrestador", x => new { x.EspecialidadesId, x.PrestadoresId });
                    table.ForeignKey(
                        name: "FK_EspecialidadePrestador_Especialidades_EspecialidadesId",
                        column: x => x.EspecialidadesId,
                        principalTable: "Especialidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EspecialidadePrestador_Prestadores_PrestadoresId",
                        column: x => x.PrestadoresId,
                        principalTable: "Prestadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consulta_ConveniadoId",
                table: "Consulta",
                column: "ConveniadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Consulta_EspecialidadeId",
                table: "Consulta",
                column: "EspecialidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Consulta_PlanoAssociadoId",
                table: "Consulta",
                column: "PlanoAssociadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Consulta_PrestadorId",
                table: "Consulta",
                column: "PrestadorId");

            migrationBuilder.CreateIndex(
                name: "IX_EspecialidadePrestador_PrestadoresId",
                table: "EspecialidadePrestador",
                column: "PrestadoresId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanoAssociado_AssociadoId",
                table: "PlanoAssociado",
                column: "AssociadoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prestadores_ConveniadoId",
                table: "Prestadores",
                column: "ConveniadoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consulta");

            migrationBuilder.DropTable(
                name: "EspecialidadePrestador");

            migrationBuilder.DropTable(
                name: "PlanoAssociado");

            migrationBuilder.DropTable(
                name: "Especialidades");

            migrationBuilder.DropTable(
                name: "Prestadores");

            migrationBuilder.DropTable(
                name: "Associados");

            migrationBuilder.DropTable(
                name: "Conveniado");
        }
    }
}
