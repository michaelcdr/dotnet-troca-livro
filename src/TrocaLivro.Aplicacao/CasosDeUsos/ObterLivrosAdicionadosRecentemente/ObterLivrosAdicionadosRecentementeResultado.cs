using System.Collections.Generic;
using TrocaLivro.Aplicacao.ViewModels;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterLivrosAdicionadosRecentementeResultado
    {
        public ObterLivrosAdicionadosRecentementeResultado(List<LivroCardModel> livros)
        {
            Livros = livros ?? new List<LivroCardModel>();
        }

        public List<LivroCardModel> Livros { get; set; }
    }
}
