using Microsoft.EntityFrameworkCore.Migrations;

namespace TrocaLivro.Infra.Migrations
{
    public partial class EnderecoTroca : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnderecoId",
                table: "Trocas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trocas_EnderecoId",
                table: "Trocas",
                column: "EnderecoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trocas_Enderecos_EnderecoId",
                table: "Trocas",
                column: "EnderecoId",
                principalTable: "Enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trocas_Enderecos_EnderecoId",
                table: "Trocas");

            migrationBuilder.DropIndex(
                name: "IX_Trocas_EnderecoId",
                table: "Trocas");

            migrationBuilder.DropColumn(
                name: "EnderecoId",
                table: "Trocas");
        }
    }
}
