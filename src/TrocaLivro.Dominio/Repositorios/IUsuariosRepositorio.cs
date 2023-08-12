using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Dominio.Repositorios
{
    public interface IUsuariosRepositorio : IRepositorio<Usuario>
    {
        Task<Usuario> ObterPorLogin(string usuario);
        void Atualizar(Usuario usuario);
    }

}
