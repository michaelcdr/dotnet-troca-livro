using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Aplicacao.CasosDeUsos.LogarUsuario
{
    public class UsuarioAutenticado
    {
        public string Token { get; private set; }
        public string Id { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string Tipo { get; private set; }
        public string Nome { get; private set; }

        public UsuarioAutenticado(Usuario usuario, string tipoUsuario, string token)
        {
            this.Token = token;
            this.Id = usuario.Id;
            this.Email = usuario.Email;
            this.UserName = usuario.UserName;
            this.Tipo = tipoUsuario;
            this.Nome = usuario.Nome;
        }
    }
}
