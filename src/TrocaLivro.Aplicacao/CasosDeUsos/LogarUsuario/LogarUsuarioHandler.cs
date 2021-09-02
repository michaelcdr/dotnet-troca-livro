using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Infra.Services;

namespace TrocaLivro.Aplicacao.CasosDeUsos.LogarUsuario
{
    public class LogarUsuarioHandler : IRequestHandler<LogarUsuarioCommand, AppResponse<LogarUsuarioResultado>>
    {
        private readonly IGeradorToken _geradorToken;
        public LogarUsuarioHandler(IGeradorToken geradorToken)
        {
            this._geradorToken = geradorToken;
        }

        public async Task<AppResponse<LogarUsuarioResultado>> Handle(LogarUsuarioCommand request, CancellationToken cancellationToken)
        {
            AppResponse<TokenResultado> tokenResultado = await _geradorToken.Gerar(request.Usuario);

            if (!tokenResultado.Sucesso)
                return new AppResponse<LogarUsuarioResultado>(tokenResultado.Mensagem, false, tokenResultado.Erros);

            var resultado = new LogarUsuarioResultado(tokenResultado.Dados.Token);

            return new AppResponse<LogarUsuarioResultado>(true, "Logado com sucesso. ",resultado);
        }
    }
}