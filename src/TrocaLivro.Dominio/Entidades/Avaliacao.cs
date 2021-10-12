using System;
using TrocaLivro.Dominio.Enums;

namespace TrocaLivro.Dominio.Entidades
{
    public class Avaliacao : EntidadeBase
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
            return this._erros.Count == 0;  
        }
    }
}
