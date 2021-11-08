using System.Collections.Generic;
using TrocaLivro.Aplicacao.ViewModels;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterTrocasDisponibilizadasPorUsuarioResultado
    {
        public List<TrocaDisponibilizadaViewModel> Livros { get; set; }

        public ObterTrocasDisponibilizadasPorUsuarioResultado(List<TrocaDisponibilizadaViewModel> livros)
        {
            Livros = livros;
        }
    }
}
