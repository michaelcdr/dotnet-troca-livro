using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Enums;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Dominio.Transacoes;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class AvaliarLivroCommandHandler : IRequestHandler<AvaliarLivroCommand, AppResponse<AvaliarLivroResultado>>
    {
        private readonly IUnitOfWork uow;

        public AvaliarLivroCommandHandler(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<AppResponse<AvaliarLivroResultado>> Handle(AvaliarLivroCommand comando, CancellationToken cancellationToken)
        {
            var response = new AvaliarLivroResultado();
            Usuario usuario = await uow.Usuarios.ObterPorLogin(comando.Usuario);

            if (usuario == null) return new AppResponse<AvaliarLivroResultado>(false, "O usuário informado na requisição não existe.", response);

            var avaliacao = new Avaliacao(comando.LivroId, usuario.Id, comando.Titulo, comando.Descricao, (NotaLivroEnum)comando.Nota);


            await uow.Livros.Avaliar(avaliacao);
            await uow.CommitAsync();

            return new AppResponse<AvaliarLivroResultado>(true, "Livro avaliado com sucesso.", response);
        }
    }
}