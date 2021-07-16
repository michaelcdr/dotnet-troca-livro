using Microsoft.EntityFrameworkCore.Migrations;

namespace TrocaLivro.Infra.Migrations
{
    public partial class livrosautoresrename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LivroAutor_Autores_AutorId",
                table: "LivroAutor");

            migrationBuilder.DropForeignKey(
                name: "FK_LivroAutor_Livros_LivroId",
                table: "LivroAutor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LivroAutor",
                table: "LivroAutor");

            migrationBuilder.RenameTable(
                name: "LivroAutor",
                newName: "LivrosAutores");

            migrationBuilder.RenameIndex(
                name: "IX_LivroAutor_LivroId",
                table: "LivrosAutores",
                newName: "IX_LivrosAutores_LivroId");

            migrationBuilder.RenameIndex(
                name: "IX_LivroAutor_AutorId",
                table: "LivrosAutores",
                newName: "IX_LivrosAutores_AutorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LivrosAutores",
                table: "LivrosAutores",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LivrosAutores_Autores_AutorId",
                table: "LivrosAutores",
                column: "AutorId",
                principalTable: "Autores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LivrosAutores_Livros_LivroId",
                table: "LivrosAutores",
                column: "LivroId",
                principalTable: "Livros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LivrosAutores_Autores_AutorId",
                table: "LivrosAutores");

            migrationBuilder.DropForeignKey(
                name: "FK_LivrosAutores_Livros_LivroId",
                table: "LivrosAutores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LivrosAutores",
                table: "LivrosAutores");

            migrationBuilder.RenameTable(
                name: "LivrosAutores",
                newName: "LivroAutor");

            migrationBuilder.RenameIndex(
                name: "IX_LivrosAutores_LivroId",
                table: "LivroAutor",
                newName: "IX_LivroAutor_LivroId");

            migrationBuilder.RenameIndex(
                name: "IX_LivrosAutores_AutorId",
                table: "LivroAutor",
                newName: "IX_LivroAutor_AutorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LivroAutor",
                table: "LivroAutor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LivroAutor_Autores_AutorId",
                table: "LivroAutor",
                column: "AutorId",
                principalTable: "Autores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LivroAutor_Livros_LivroId",
                table: "LivroAutor",
                column: "LivroId",
                principalTable: "Livros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
