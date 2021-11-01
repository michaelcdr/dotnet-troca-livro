using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterUsuarioResultado
    {
        public string Login { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public static ObterUsuarioResultado CriarPor(Usuario usuario)
        {
            return new ObterUsuarioResultado
            {
                Email = usuario.Email,
                Nome = usuario.Nome,
                Login = usuario.UserName,
                Sobrenome = usuario.Sobrenome,
                Avatar = usuario.Avatar
            };
        }
    }
}
