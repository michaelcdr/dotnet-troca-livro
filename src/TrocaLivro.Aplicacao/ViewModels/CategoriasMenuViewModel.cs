using System.Collections.Generic;
using TrocaLivro.Aplicacao.CasosDeUsos;

namespace TrocaLivro.Aplicacao.ViewModels
{
    public class CategoriasMenuViewModel
    {
        public List<CategoriaViewModel> Categorias { get; set; }

        public CategoriasMenuViewModel(List<CategoriaViewModel> categorias)
        {
            Categorias = categorias;
        }
    }
}
