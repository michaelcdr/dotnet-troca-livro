using System.Collections.Generic;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class CategoriaViewModel
    {
        public int CategoriaId { get; set; }
        public string Nome { get; set; }
        public List<SubCategoriaViewModel> SubCategorias { get; set; }
    }
}
