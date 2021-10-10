using System;
using TrocaLivro.Dominio.Enums;

namespace TrocaLivro.Dominio.Entidades
{
    public class LivroDisponibilizadoParaTroca : EntidadeBase
    {
        public int Id { get; set; }
        public int LivroId { get; set; }
        public Livro Livro { get; set; }
        public int Pontos { get; set; }
        public DateTime DisponibilizadoEm { get; set; }
        public string UsuarioQueDisponibilizouParaTrocaId { get; set; }
        public Usuario UsuarioQueDisponibilizouParaTroca { get; set; }
        public StatusTroca Status { get; set; }
        public string Descritivo { get; set; }

        public override bool TaValido()
        {
            if (string.IsNullOrEmpty(Descritivo))
                this.AdicionarErro("Informe o descritivo",nameof(Descritivo));

            if (this.Pontos <= 0 || this.Pontos > 3)
                this.AdicionarErro("Numero de pontos inválidos, deve ser > 0 e <= 3", nameof(Pontos));

            if (this.LivroId == 0)
                this.AdicionarErro("Informe o livro", nameof(LivroId));

            return this._erros.Count == 0;
        }
    }
}