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

        public void Atualizar(Usuario usuario)
        {
            ApplicationDbContext.Usuarios.Update(usuario);
        }

        public async Task<Usuario> ObterPorLogin(string usuario)
        {
            return await ApplicationDbContext.Usuarios
                .Include(e => e.AvaliacoesFeitas)
                .SingleOrDefaultAsync(usuarioAtual => usuarioAtual.UserName == usuario);
        }
    }
}
