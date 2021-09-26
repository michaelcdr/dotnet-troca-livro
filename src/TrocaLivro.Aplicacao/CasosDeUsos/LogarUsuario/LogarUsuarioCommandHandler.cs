using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Infra.Services;

namespace TrocaLivro.Aplicacao.CasosDeUsos.LogarUsuario
{
    public class LogarUsuarioCommandHandler : IRequestHandler<LogarUsuarioCommand, AppResponse<LogarUsuarioResultado>>
    {
        private readonly IGerenciadorToken _geradorToken;
        public LogarUsuarioCommandHandler(IGerenciadorToken geradorToken)
        {
            this._geradorToken = geradorToken;
        }

        public async Task<AppResponse<LogarUsuarioResultado>> Handle(LogarUsuarioCommand request, CancellationToken cancellationToken)
        {
            AppResponse<TokenResultado> tokenResultado = await _geradorToken.Gerar(request.Usuario);

            if (!tokenResultado.Sucesso)
                return new AppResponse<LogarUsuarioResultado>(tokenResultado.Mensagem, false, tokenResultado.Erros);

            var resultado = new LogarUsuarioResultado(tokenResultado.Dados.Token,tokenResultado.Dados.Role);

            return new AppResponse<LogarUsuarioResultado>(true, "Logado com sucesso. ",resultado);
        }
    }
}