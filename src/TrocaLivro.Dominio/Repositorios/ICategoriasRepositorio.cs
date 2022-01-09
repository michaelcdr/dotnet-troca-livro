using System.Collections.Generic;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Dominio.Repositorios
{
    public interface ICategoriasRepositorio : IRepositorio<Categoria>
    {
        Task<IList<SubCategoria>> ObterSubCategorias(int categoriaId);
        Task<IList<Categoria>> ObterTodas();
        Task<IList<SubCategoria>> ObterTodasSubCategorias();
        Task<bool> Existe(string nome);
        Task<Categoria> ObterPorNome(string nome);
        void AdicionarSubCategoria(SubCategoria subCategoria);
    }
}
