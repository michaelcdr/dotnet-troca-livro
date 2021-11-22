using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos.ObterTodosAutores
{
    public class ObterTodosAutoresQuery : IRequest<AppResponse<ObterTodosAutoresResultado>>
    {
    }
}
