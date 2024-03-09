using System;
using TrocaLivro.Dominio.Enums;

namespace TrocaLivro.Dominio.Entidades
{
    public class Avaliacao : Entidade
    {
        public int Id { get; set; }
        public string UsuarioId { get; private set; }
        public Usuario Usuario { get; set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public NotaLivroEnum Nota { get; private set; }
        public DateTime AvaliadoEm { get; private set; }
        public int LivroId { get; private set; }
        public Livro Livro { get; set; }

        public Avaliacao(int livroId, string usuarioId, string titulo,string descricao, NotaLivroEnum nota)
        {
            this.LivroId = livroId;
            this.UsuarioId = usuarioId;
            this.Titulo = titulo;
            this.Descricao = descricao;
            this.Nota = nota;
            this.AvaliadoEm = DateTime.Now;
        }

        public override bool TaValido()
        {
            bool estaNoLimiteDeNotasPermitidas = this.Nota.GetHashCode() > 0 && this.Nota.GetHashCode() <= 5;

            if (this.Nota.GetHashCode() == 0)
                this.AdicionarErro("Informe uma nota.", nameof(this.Nota));
            else if (!estaNoLimiteDeNotasPermitidas)
                this.AdicionarErro("Informe uma nota válida.", nameof(this.Nota));

            return this._erros.Count == 0;  
        }
    }
}