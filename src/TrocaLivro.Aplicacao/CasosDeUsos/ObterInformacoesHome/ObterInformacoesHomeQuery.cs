using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos.ObterInformacoesHome
{
    public class ObterInformacoesHomeQuery : IRequest<AppResponse<ObterInformacoesHomeQueryResultado>>
    {
    }
}
