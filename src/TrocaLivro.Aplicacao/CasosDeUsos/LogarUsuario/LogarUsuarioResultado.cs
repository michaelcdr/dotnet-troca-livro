namespace TrocaLivro.Aplicacao.CasosDeUsos.LogarUsuario
{
    public class LogarUsuarioResultado
    {
        public string Token { get; private set; }
        public string Role { get; private set; }
        public LogarUsuarioResultado(string token, string role)
        {
            this.Token = token;
            this.Role = role;
        }
    }
}
