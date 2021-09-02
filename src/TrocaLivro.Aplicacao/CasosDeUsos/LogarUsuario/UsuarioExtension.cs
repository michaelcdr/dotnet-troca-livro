using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Aplicacao.CasosDeUsos.LogarUsuario
{
    public static class UsuarioExtension
    {
        public static UsuarioAutenticado ToAutenticado(this Usuario usuario, string tipoUsuario, string token)
        {
            return new UsuarioAutenticado(usuario,tipoUsuario,token);
        }
    }
}