using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.ViewModels;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Dominio.Transacoes;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterLivrosQueryHandler : IRequestHandler<ObterLivrosQuery, AppResponse<ObterLivrosResultado>>
    {
        private readonly IUnitOfWork uow;
        private const string MSG_SUCCESS = "Livros obtidos com sucesso.";
        private readonly IMapper mapper;
        public ObterLivrosQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.uow = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<AppResponse<ObterLivrosResultado>> Handle(ObterLivrosQuery request, CancellationToken cancellationToken)
        {
            List<Livro> livros = await uow.Livros.ObterLivrosComAutores(
                request.TamanhoPagina,
                request.QuantidadeRegistrosAPular, 
                request.TermoPesquisa);

            List<LivroCardModel> livrosCards = this.mapper.Map<List<Livro>, List<LivroCardModel>>(livros);

            var resultado = new ObterLivrosResultado(livrosCards);

            return new AppResponse<ObterLivrosResultado>(true, MSG_SUCCESS, resultado);
        }
    }
}
