using TrocaLivro.Dominio.Repositorios;
using System.Threading.Tasks;

namespace TrocaLivro.Dominio.Transacoes
{
    public interface IUnitOfWork
    {
        ILivrosRepositorio Livros { get; } 
        IUsuariosRepositorio Usuarios { get; }
        IAutoresRepositorio Autores { get; }
        IEditorasRepositorio Editoras { get; }
        ICategoriasRepositorio Categorias { get; }
        Task<int> CommitAsync();
    }
}
