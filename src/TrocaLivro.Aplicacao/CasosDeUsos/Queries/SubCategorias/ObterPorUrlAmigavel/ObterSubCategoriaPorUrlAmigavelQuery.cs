using MediatR;
using TrocaLivro.Aplicacao.DTO;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterSubCategoriaPorUrlAmigavelQuery : IRequest<AppResponse<SubCategoriaDTO>>
    {
        public string UrlAmigavel { get; set; }

        public ObterSubCategoriaPorUrlAmigavelQuery(string urlAmigavel)
        {
            UrlAmigavel = urlAmigavel;
        }
    }
}
