using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Infra.Data;

namespace TrocaLivro.Aplicacao.CasosDeUsos.ObterUsuario
{
    public class ObterUsuarioQueryHandler : IRequestHandler<ObterUsuarioQuery, AppResponse<ObterUsuarioResultado>>
    {
        private readonly ApplicationDbContext context;

        public ObterUsuarioQueryHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<AppResponse<ObterUsuarioResultado>> Handle(ObterUsuarioQuery request, CancellationToken cancellationToken)
        {
            Usuario usuario = await context.Usuarios.SingleAsync(usuarioAtual => usuarioAtual.UserName == request.UserName);
            ObterUsuarioResultado resultado = ObterUsuarioResultado.CriarPor(usuario);
            return new AppResponse<ObterUsuarioResultado>(true, "Usuário obtido com sucesso.", resultado);
        }
    }
}
