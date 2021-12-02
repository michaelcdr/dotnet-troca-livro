using MediatR;
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

        public ObterLivroQueryHandler(IUnitOfWork unitOfWork)
        {
            this.uow = unitOfWork;
        }

        public async Task<AppResponse<ObterLivroResultado>> Handle(ObterLivroQuery request, CancellationToken cancellationToken)
        {
            Livro livro = await uow.Livros.Obter(request.LivroId);

            LivroDTO livroDTO = livro.ToDTO();

            var resultado = new ObterLivroResultado(livroDTO);

            return new AppResponse<ObterLivroResultado>(true, MSG_SUCCESS, resultado);
        }
    }
}