using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos.LogarUsuario
{
    public class LogarUsuarioCommand : IRequest<AppResponse<LogarUsuarioResultado>>
    {
        public LogarUsuarioCommand(string usuario, string senha)
        {
            Usuario = usuario;
            Senha = senha;
        }

        public string Usuario { get; private set; }
        public string Senha { get; private set; }
    }
}
