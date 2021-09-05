using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Dominio.Transacoes;

namespace TrocaLivro.Aplicacao.CasosDeUsos.ObterInformacoesHome
{
    public class ObterInformacoesHomeQueryHandler : IRequestHandler<ObterInformacoesHomeQuery, AppResponse<ObterInformacoesHomeQueryResultado>>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork uow;

        public ObterInformacoesHomeQueryHandler(IMapper mapper, IUnitOfWork uow )
        {
            this.mapper = mapper;
            this.uow = uow;
        }

        public async Task<AppResponse<ObterInformacoesHomeQueryResultado>> Handle(ObterInformacoesHomeQuery request, CancellationToken cancellationToken)
        {
            const int NUMERO_LIVROS = 4;

            var ultimosQuatroLivros = await uow.Livros.ObterUltimosLivrosCadastrados(NUMERO_LIVROS);

            return new AppResponse<ObterInformacoesHomeQueryResultado>();
        }
    }
}
