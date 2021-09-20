using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterLivrosAdicionadosRecentementeQuery : IRequest<AppResponse<ObterLivrosAdicionadosRecentementeResultado>>
    {
        
    }
}
