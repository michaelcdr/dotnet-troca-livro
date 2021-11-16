using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.ViewModels;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Dominio.Transacoes;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterAvaliacoesLivroQueryHandler : IRequestHandler<ObterAvaliacoesLivroQuery, AppResponse<ObterAvaliacoesLivroResultado>>
    {
        private readonly IUnitOfWork uow;
        
        public ObterAvaliacoesLivroQueryHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<AppResponse<ObterAvaliacoesLivroResultado>> Handle(ObterAvaliacoesLivroQuery request, CancellationToken cancellationToken)
        {
            List<Avaliacao> avaliacaos = await uow.Livros.ObterAvaliacoes(request.LivroId);

            List<AvaliacaoLivro> avaliacoesDoLivro = avaliacaos
                .Select(e => new AvaliacaoLivro(e.Titulo, e.Descricao, e.Nota, e.AvaliadoEm, e.Usuario.Nome))
                .OrderByDescending(e => e.AvaliadoEm)
                .ToList();

            var resultado = new ObterAvaliacoesLivroResultado(avaliacoesDoLivro);

            return new AppResponse<ObterAvaliacoesLivroResultado>(true,"Livros obtidos com sucesso.", resultado);
        }
    }
}