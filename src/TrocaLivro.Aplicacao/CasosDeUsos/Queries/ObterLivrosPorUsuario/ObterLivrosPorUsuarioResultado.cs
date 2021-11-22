using System.Collections.Generic;
using TrocaLivro.Aplicacao.ViewModels;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterLivrosPorUsuarioResultado
    {
        public List<LivroCardModel> Livros { get; set; }

        public ObterLivrosPorUsuarioResultado(List<LivroCardModel> livros)
        {
            this.Livros = livros;
        }
    }
}
