using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Dominio.Transacoes;
using TrocaLivro.Infra.Repositorios.Data;

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

            var pacotes = new PacotesRepositorio().ObterTodos().Select(pacoteAtual => new PacotePontosViewModel { 
                Descritivo  = pacoteAtual.Descritivo,
                Id = pacoteAtual.Id,
                Pontos = pacoteAtual.Pontos,
                Titulo = pacoteAtual.Titulo,
                Valor = pacoteAtual.Valor
            }).ToList();

            var resultado = new ObterDadosDashboardResultado
            {
                QtdLivrosCadastrados = qtdLivrosCadastrados,
                QtdLivrosDisponiveis = qtdLivrosDisponiveisParaTroca,
                QtdLivrosTrocados = qtdLivrosTrocados,
                Pacotes = pacotes
            };

            return new AppResponse<ObterDadosDashboardResultado>(true, "Total de livros obtido com sucesso.", resultado);
        }
    }
}
