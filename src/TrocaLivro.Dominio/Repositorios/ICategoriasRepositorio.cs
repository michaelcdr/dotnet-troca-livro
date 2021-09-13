using System.Collections.Generic;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Dominio.Repositorios
{
    public interface ICategoriasRepositorio : IRepositorio<Categoria>
    {
        Task<IList<SubCategoria>> ObterSubCategorias(int categoriaId);
        Task<IList<Categoria>> ObterTodas();
    }
}
