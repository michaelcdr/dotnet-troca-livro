using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterLivrosPorUsuarioQuery : IRequest<AppResponse<ObterLivrosPorUsuarioResultado>>
    {
        public string Usuario { get; set; }
    }
}
