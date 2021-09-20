using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Dominio.Transacoes;

namespace TrocaLivro.Aplicacao.CasosDeUsos.DeletarLivro
{
    public class DeletarLivroCommandHandler : IRequestHandler<DeletarLivroCommand, AppResponse<DeletarLivroResultado>>
    {
        private readonly IUnitOfWork _uow;

        public DeletarLivroCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<AppResponse<DeletarLivroResultado>> Handle(DeletarLivroCommand request, CancellationToken cancellationToken)
        {
            Livro livro = await _uow.Livros.Get(request.LivroId);
            livro.Deletar(request.Usuario);
            await _uow.CommitAsync();

            return new AppResponse<DeletarLivroResultado>(true, "Livro deletado com sucesso.");
        }
    }
}
