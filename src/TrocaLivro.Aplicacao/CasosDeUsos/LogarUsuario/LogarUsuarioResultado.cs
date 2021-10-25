using TrocaLivro.Infra.Services;

namespace TrocaLivro.Aplicacao.CasosDeUsos.LogarUsuario
{
    public class LogarUsuarioResultado
    {
        public string Token { get; private set; }
        public string Role { get; private set; }
        public string UsuarioId { get; private set; }
        public LogarUsuarioResultado(string token, string role, string usuarioId)
        {
            this.Token = token;
            this.Role = role;
            this.UsuarioId = usuarioId;
        }
    }
}
