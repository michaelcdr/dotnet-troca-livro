using System.Collections.Generic;
using TrocaLivro.Aplicacao.ViewModels;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterLivrosResultado
    {
        public ObterLivrosResultado(List<LivroCardModel> livros)
        {
            Livros = livros ?? new List<LivroCardModel>();
        }

        public List<LivroCardModel> Livros { get; set; }
    }
}
