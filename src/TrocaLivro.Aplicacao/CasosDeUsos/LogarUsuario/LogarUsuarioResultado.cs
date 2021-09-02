namespace TrocaLivro.Aplicacao.CasosDeUsos.LogarUsuario
{
    public class LogarUsuarioResultado
    {
        public string Token { get; private set; }
        public LogarUsuarioResultado(string token)
        {
            this.Token = token;
        }
    }
}
