using System.Collections.Generic;

namespace TrocaLivro.Dominio.Entidades
{
    public class Editora : EntidadeBase
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }

        public List<Livro> Livros { get; private set; }
        
        public Editora(string nome)
        {
            this.Livros = new List<Livro>();
            this.Nome = nome;
        }

        public override bool TaValido()
        {
            if (string.IsNullOrEmpty(this.Nome))
                this._erros.Add(new Notificacao("O nome deve ser informado", "Nome"));

            return this._erros.Count == 0;
        }
    }
}
