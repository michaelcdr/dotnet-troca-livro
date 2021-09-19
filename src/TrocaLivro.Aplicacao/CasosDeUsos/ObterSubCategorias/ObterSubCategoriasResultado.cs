using System.Collections.Generic;
using TrocaLivro.Dominio.DTO;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterSubCategoriasResultado
    {
        public ObterSubCategoriasResultado(List<SubCategoriaDTO> subCategoriaDTOs)
        {
            SubCategorias = subCategoriaDTOs;
        }

        public List<SubCategoriaDTO> SubCategorias { get; private set; }
    }
}