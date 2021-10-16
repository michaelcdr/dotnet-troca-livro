using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterAvaliacoesLivroQuery : IRequest<AppResponse<ObterAvaliacoesLivroResultado>>
    {
        public int LivroId { get; set; }
    }
}
