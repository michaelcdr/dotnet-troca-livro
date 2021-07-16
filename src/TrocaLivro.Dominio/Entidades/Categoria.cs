using System.Collections.Generic;

namespace TrocaLivro.Dominio.Entidades
{
    public class Categoria:EntidadeBase
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public List<Livro> Livros { get; set; }

        public override bool TaValido()
        {
            return true;
        }
    }
}
