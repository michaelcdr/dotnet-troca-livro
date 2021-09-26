using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.DTO;
using TrocaLivro.Aplicacao.Extensions;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Dominio.Transacoes;

namespace TrocaLivro.Aplicacao.CasosDeUsos.ObterLivro
{
    public class ObterLivroQueryHandler : IRequestHandler<ObterLivroQuery, AppResponse<ObterLivroResultado>>
    {
        private readonly IUnitOfWork uow;
        private const string MSG_SUCCESS = "Livro obtido com sucesso.";
        private readonly IMapper mapper;

        public ObterLivroQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.uow = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<AppResponse<ObterLivroResultado>> Handle(ObterLivroQuery request, CancellationToken cancellationToken)
        {
            Livro livro = await uow.Livros.Obter(request.LivroId);

            LivroDTO livroDTO = livro.ToDTO();

            var resultado = new ObterLivroResultado() { Livro = livroDTO };

            return new AppResponse<ObterLivroResultado>(true, MSG_SUCCESS, resultado);
        }
    }
}
