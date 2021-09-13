using System.Collections.Generic;
using TrocaLivro.Dominio.DTO;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterSubCategoriasResultado
    {
        public ObterSubCategoriasResultado(List<SubCategoriaDTO> subCategoriaDTOs)
        {
            SubCategoriaDTOs = subCategoriaDTOs;
        }

        public List<SubCategoriaDTO> SubCategoriaDTOs { get; private set; }
    }
}