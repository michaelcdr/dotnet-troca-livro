using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Dominio.Transacoes;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterDadosDashboardQueryHandler : IRequestHandler<ObterDadosDashboardQuery, AppResponse<ObterDadosDashboardResultado>>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork uow;

        public ObterDadosDashboardQueryHandler(IMapper mapper, IUnitOfWork uow )
        {
            this.mapper = mapper;
            this.uow = uow;
        }

        public async Task<AppResponse<ObterDadosDashboardResultado>> Handle(ObterDadosDashboardQuery request, CancellationToken cancellationToken)
        {
            int qtdLivrosCadastrados = await uow.Livros.ObterTotal();
            int qtdLivrosTrocados = await uow.Livros.ObterTotalDeTrocas();
            int qtdLivrosDisponiveisParaTroca = await uow.Livros.ObterTotalLivrosDisponiveisParaTroca();
            var resultado = new ObterDadosDashboardResultado
            {
                QtdLivrosCadastrados = qtdLivrosCadastrados,
                QtdLivrosDisponiveis = qtdLivrosDisponiveisParaTroca,
                QtdLivrosTrocados = qtdLivrosTrocados
            };

            return new AppResponse<ObterDadosDashboardResultado>(true, "Total de livros obtido com sucesso.", resultado);
        }
    }
}
