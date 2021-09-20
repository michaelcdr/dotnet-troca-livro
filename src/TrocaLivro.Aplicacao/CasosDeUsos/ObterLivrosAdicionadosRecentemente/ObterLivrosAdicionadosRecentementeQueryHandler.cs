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

namespace TrocaLivro.Aplicacao.CasosDeUsos.ObterLivrosAdicionadosRecentemente
{
    public class ObterLivrosAdicionadosRecentementeQueryHandler : IRequestHandler<ObterLivrosAdicionadosRecentementeQuery, AppResponse<ObterLivrosAdicionadosRecentementeResultado>>
    {
        private readonly IUnitOfWork uow;
        private const string MSG_SUCCESS = "Livros obtidos com sucesso.";
        private readonly IMapper mapper;
        public ObterLivrosAdicionadosRecentementeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.uow = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<AppResponse<ObterLivrosAdicionadosRecentementeResultado>> Handle(ObterLivrosAdicionadosRecentementeQuery request, CancellationToken cancellationToken)
        {
            List<Livro> livros = await uow.Livros.ObterLivrosComAutores();

            List<LivroCardModel> livrosCards = this.mapper.Map<List<Livro>, List<LivroCardModel>>(livros);

            var resultado = new ObterLivrosAdicionadosRecentementeResultado(livrosCards);

            return new AppResponse<ObterLivrosAdicionadosRecentementeResultado>(true, MSG_SUCCESS, resultado);
        }
    }
}
