using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Dominio.Transacoes;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class AvaliarLivroCommandHandler : IRequestHandler<AvaliarLivroCommand, AppResponse<AvaliarLivroResultado>>
    {
        private readonly IUnitOfWork _uow;
        private const string sucesso = "Livro avaliado com sucesso.";
        public AvaliarLivroCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<AppResponse<AvaliarLivroResultado>> Handle(AvaliarLivroCommand comando, 
                                                                     CancellationToken cancellationToken)
        {
            var response = new AvaliarLivroResultado();

            Usuario usuario = await _uow.Usuarios.ObterPorLogin(comando.Usuario);

            if (usuario == null) 
                return new AppResponse<AvaliarLivroResultado>(false, "O usuário informado na requisição não existe.", response);

            usuario.AvaliarLivro(
                comando.LivroId,                 
                comando.Titulo, 
                comando.Descricao, 
                comando.Nota
            );

            _uow.Usuarios.Atualizar(usuario);

            await _uow.CommitAsync();

            return new AppResponse<AvaliarLivroResultado>(true, sucesso, response);
        }
    }
}