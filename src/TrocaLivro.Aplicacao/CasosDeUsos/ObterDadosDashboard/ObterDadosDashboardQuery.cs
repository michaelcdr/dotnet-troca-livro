using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterDadosDashboardQuery : IRequest<AppResponse<ObterDadosDashboardResultado>>
    {
        public ObterDadosDashboardQuery(int quantidadeResultados)
        {
            QuantidadeResultados = quantidadeResultados;
        }

        public int QuantidadeResultados { get; set; }
    }
}