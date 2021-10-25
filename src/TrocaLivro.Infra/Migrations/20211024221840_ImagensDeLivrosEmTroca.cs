using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrocaLivro.Infra.Migrations
{
    public partial class ImagensDeLivrosEmTroca : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImagensLivrosEmTroca",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrocaId = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagensLivrosEmTroca", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagensLivrosEmTroca_LivrosDisponibilizadosParaTrocas_TrocaId",
                        column: x => x.TrocaId,
                        principalTable: "LivrosDisponibilizadosParaTrocas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImagensLivrosEmTroca_TrocaId",
                table: "ImagensLivrosEmTroca",
                column: "TrocaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImagensLivrosEmTroca");
        }
    }
}
