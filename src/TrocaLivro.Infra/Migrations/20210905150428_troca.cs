using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrocaLivro.Infra.Migrations
{
    public partial class troca : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LivrosDisponibilizadosParaTrocas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LivroId = table.Column<int>(type: "int", nullable: false),
                    Pontos = table.Column<int>(type: "int", nullable: false),
                    DisponibilizadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DisponibilizadoPor = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Descritivo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
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
                        name: "FK_LivrosDisponibilizadosParaTrocas_Usuarios_DisponibilizadoPor",
                        column: x => x.DisponibilizadoPor,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LivrosDisponibilizadosParaTrocas_DisponibilizadoPor",
                table: "LivrosDisponibilizadosParaTrocas",
                column: "DisponibilizadoPor");

            migrationBuilder.CreateIndex(
                name: "IX_LivrosDisponibilizadosParaTrocas_LivroId",
                table: "LivrosDisponibilizadosParaTrocas",
                column: "LivroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LivrosDisponibilizadosParaTrocas");
        }
    }
}
