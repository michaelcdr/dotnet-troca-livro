using Microsoft.EntityFrameworkCore.Migrations;

namespace TrocaLivro.Infra.Migrations
{
    public partial class ColumnRenameInDisponibilizacaoTroca : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LivrosDisponibilizadosParaTrocas_Usuarios_DisponibilizadoPor",
                table: "LivrosDisponibilizadosParaTrocas");

            migrationBuilder.RenameColumn(
                name: "DisponibilizadoPor",
                table: "LivrosDisponibilizadosParaTrocas",
                newName: "UsuarioQueDisponibilizouParaTrocaId");

            migrationBuilder.RenameIndex(
                name: "IX_LivrosDisponibilizadosParaTrocas_DisponibilizadoPor",
                table: "LivrosDisponibilizadosParaTrocas",
                newName: "IX_LivrosDisponibilizadosParaTrocas_UsuarioQueDisponibilizouParaTrocaId");

            migrationBuilder.AddForeignKey(
                name: "FK_LivrosDisponibilizadosParaTrocas_Usuarios_UsuarioQueDisponibilizouParaTrocaId",
                table: "LivrosDisponibilizadosParaTrocas",
                column: "UsuarioQueDisponibilizouParaTrocaId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LivrosDisponibilizadosParaTrocas_Usuarios_UsuarioQueDisponibilizouParaTrocaId",
                table: "LivrosDisponibilizadosParaTrocas");

            migrationBuilder.RenameColumn(
                name: "UsuarioQueDisponibilizouParaTrocaId",
                table: "LivrosDisponibilizadosParaTrocas",
                newName: "DisponibilizadoPor");

            migrationBuilder.RenameIndex(
                name: "IX_LivrosDisponibilizadosParaTrocas_UsuarioQueDisponibilizouParaTrocaId",
                table: "LivrosDisponibilizadosParaTrocas",
                newName: "IX_LivrosDisponibilizadosParaTrocas_DisponibilizadoPor");

            migrationBuilder.AddForeignKey(
                name: "FK_LivrosDisponibilizadosParaTrocas_Usuarios_DisponibilizadoPor",
                table: "LivrosDisponibilizadosParaTrocas",
                column: "DisponibilizadoPor",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
