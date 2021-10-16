using System.Collections.Generic;
using TrocaLivro.Aplicacao.ViewModels;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterAvaliacoesLivroResultado
    {
        public ObterAvaliacoesLivroResultado(List<AvaliacaoLivro> avaliacoesDoLivro)
        {
            AvaliacoesDoLivro = avaliacoesDoLivro;
        }

        public List<AvaliacaoLivro> AvaliacoesDoLivro { get;  set; }
    }
}
