using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterTodasEditorasQuery : IRequest<AppResponse<ObterTodasEditorasResultado>>
    {

    }
}
