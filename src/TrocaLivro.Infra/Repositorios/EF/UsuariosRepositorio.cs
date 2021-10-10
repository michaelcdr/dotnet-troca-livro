using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Repositorios;
using TrocaLivro.Infra.Data;

namespace TrocaLivro.Infra.Repositorios.EF
{
    public class UsuariosRepositorio : Repositorio<Usuario>, IUsuariosRepositorio
    { 

        public UsuariosRepositorio(ApplicationDbContext context) : base(context)
        {

        }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public async Task<Usuario> ObterPorLogin(string usuario)
        {
            return await ApplicationDbContext.Usuarios.SingleOrDefaultAsync(usuarioAtual => usuarioAtual.UserName == usuario);
        }
    }
}
