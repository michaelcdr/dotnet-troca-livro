using System.Collections.Generic;

namespace TrocaLivro.Dominio.Entidades
{
    public class SubCategoria : EntidadeBase
    {
        public int Id { get; set; }
        public int CategoriaId { get; set; }
        public string Nome { get; set; }
        public List<Livro> Livros { get; set; }
        public Categoria Categoria { get; set; }

        public SubCategoria(string nome)
        {
            this.Nome = nome;
        }
        public override bool TaValido()
        {
            throw new System.NotImplementedException();
        }
    }
}
