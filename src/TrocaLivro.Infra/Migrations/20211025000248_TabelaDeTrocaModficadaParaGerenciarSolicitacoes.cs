using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrocaLivro.Infra.Migrations
{
    public partial class TabelaDeTrocaModficadaParaGerenciarSolicitacoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagensLivrosEmTroca_LivrosDisponibilizadosParaTrocas_TrocaId",
                table: "ImagensLivrosEmTroca");

            migrationBuilder.DropTable(
                name: "LivrosDisponibilizadosParaTrocas");

            migrationBuilder.CreateTable(
                name: "Trocas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LivroId = table.Column<int>(type: "int", nullable: false),
                    Pontos = table.Column<int>(type: "int", nullable: false),
                    DisponibilizadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioQueDisponibilizouParaTrocaId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Descritivo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UsuarioQueSolicitouTrocaId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataSolicitacaoTroca = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trocas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trocas_Livros_LivroId",
                        column: x => x.LivroId,
                        principalTable: "Livros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trocas_Usuarios_UsuarioQueDisponibilizouParaTrocaId",
                        column: x => x.UsuarioQueDisponibilizouParaTrocaId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trocas_Usuarios_UsuarioQueSolicitouTrocaId",
                        column: x => x.UsuarioQueSolicitouTrocaId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trocas_LivroId",
                table: "Trocas",
                column: "LivroId");

            migrationBuilder.CreateIndex(
                name: "IX_Trocas_UsuarioQueDisponibilizouParaTrocaId",
                table: "Trocas",
                column: "UsuarioQueDisponibilizouParaTrocaId");

            migrationBuilder.CreateIndex(
                name: "IX_Trocas_UsuarioQueSolicitouTrocaId",
                table: "Trocas",
                column: "UsuarioQueSolicitouTrocaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagensLivrosEmTroca_Trocas_TrocaId",
                table: "ImagensLivrosEmTroca",
                column: "TrocaId",
                principalTable: "Trocas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagensLivrosEmTroca_Trocas_TrocaId",
                table: "ImagensLivrosEmTroca");

            migrationBuilder.DropTable(
                name: "Trocas");

            migrationBuilder.CreateTable(
                name: "LivrosDisponibilizadosParaTrocas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descritivo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DisponibilizadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LivroId = table.Column<int>(type: "int", nullable: false),
                    Pontos = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UsuarioQueDisponibilizouParaTrocaId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivrosDisponibilizadosParaTrocas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LivrosDisponibilizadosParaTrocas_Livros_LivroId",
                        column: x => x.LivroId,
                        principalTable: "Livros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LivrosDisponibilizadosParaTrocas_Usuarios_UsuarioQueDisponibilizouParaTrocaId",
                        column: x => x.UsuarioQueDisponibilizouParaTrocaId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LivrosDisponibilizadosParaTrocas_LivroId",
                table: "LivrosDisponibilizadosParaTrocas",
                column: "LivroId");

            migrationBuilder.CreateIndex(
                name: "IX_LivrosDisponibilizadosParaTrocas_UsuarioQueDisponibilizouParaTrocaId",
                table: "LivrosDisponibilizadosParaTrocas",
                column: "UsuarioQueDisponibilizouParaTrocaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagensLivrosEmTroca_LivrosDisponibilizadosParaTrocas_TrocaId",
                table: "ImagensLivrosEmTroca",
                column: "TrocaId",
                principalTable: "LivrosDisponibilizadosParaTrocas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
