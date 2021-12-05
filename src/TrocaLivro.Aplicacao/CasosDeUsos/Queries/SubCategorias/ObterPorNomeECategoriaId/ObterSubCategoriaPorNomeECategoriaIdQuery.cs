using MediatR;
using TrocaLivro.Aplicacao.DTO;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterSubCategoriaPorNomeECategoriaIdQuery : IRequest<AppResponse<SubCategoriaDTO>>
    {
        public ObterSubCategoriaPorNomeECategoriaIdQuery(int categoriaId, string nomeSubCategoria)
        {
            CategoriaId = categoriaId;
            NomeSubCategoria = nomeSubCategoria;
        }

        public int CategoriaId { get; set; }
        public string NomeSubCategoria { get; set; }
    }
}