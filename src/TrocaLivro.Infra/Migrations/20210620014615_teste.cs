using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrocaLivro.Infra.Migrations
{
    public partial class teste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Livros_Autores_AutorId",
                table: "Livros");

            migrationBuilder.DropIndex(
                name: "IX_Livros_AutorId",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "AutorId",
                table: "Livros");

            migrationBuilder.AddColumn<string>(
                name: "AlteradoPor",
                table: "Livros",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CadastradoPor",
                table: "Livros",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "Livros",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "Livros",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Subtitulo",
                table: "Livros",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Livros",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LivroAutor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AutorId = table.Column<int>(type: "int", nullable: false),
                    LivroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivroAutor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LivroAutor_Autores_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Autores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LivroAutor_Livros_LivroId",
                        column: x => x.LivroId,
                        principalTable: "Livros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LivroAutor_AutorId",
                table: "LivroAutor",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_LivroAutor_LivroId",
                table: "LivroAutor",
                column: "LivroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LivroAutor");

            migrationBuilder.DropColumn(
                name: "AlteradoPor",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "CadastradoPor",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "Subtitulo",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Livros");

            migrationBuilder.AddColumn<int>(
                name: "AutorId",
                table: "Livros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Livros_AutorId",
                table: "Livros",
                column: "AutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_Autores_AutorId",
                table: "Livros",
                column: "AutorId",
                principalTable: "Autores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
