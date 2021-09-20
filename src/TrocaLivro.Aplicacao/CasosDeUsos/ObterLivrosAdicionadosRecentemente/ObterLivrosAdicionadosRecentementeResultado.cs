using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.ViewModels;

namespace TrocaLivro.Aplicacao.CasosDeUsos.ObterLivrosAdicionadosRecentemente
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
