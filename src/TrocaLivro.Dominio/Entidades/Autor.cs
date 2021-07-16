using System.Collections.Generic;

namespace TrocaLivro.Dominio.Entidades
{
    public class LivroAutor
    {
        public int Id { get; set; }
        public int AutorId { get; set; }
        public int LivroId { get; set; }
        public Livro Livro { get; set; }
        public Autor Autor { get; set; }
    }

    public class Autor : EntidadeBase
    {
        public Autor(string nome)
        {
            Nome = nome;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public List<LivroAutor> LivrosAutores { get; private set; }
        public override bool TaValido()
        {
            if (string.IsNullOrEmpty(this.Nome))
                this._erros.Add(new Notificacao("O nome deve ser informado", "Nome"));

            return this._erros.Count == 0;
        }
    }
}
