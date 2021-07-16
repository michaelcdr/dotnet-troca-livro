using TrocaLivro.Dominio.Repositorios;
using TrocaLivro.Dominio.Transacoes;
using TrocaLivro.Infra.Data;
using System.Threading.Tasks;

namespace TrocaLivro.Infra.Transacoes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IUsuariosRepositorio Usuarios { get; private set; }
        public ILivrosRepositorio Livros { get; private set; }
        public IAutoresRepositorio Autores { get; private set; }
        public IEditorasRepositorio Editoras { get; private set; }
        public ICategoriasRepositorio Categorias { get; private set; }
        public  async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public UnitOfWork(
            ApplicationDbContext context,
            ILivrosRepositorio livrosRepositorio,
            IUsuariosRepositorio usuariosRepositorio, 
            IEditorasRepositorio editorasRepositorio, 
            IAutoresRepositorio autoresRepositorio, 
            ICategoriasRepositorio categoriasRepositorio )
        {
            this._context = context;
            this.Livros = livrosRepositorio;
            this.Usuarios = usuariosRepositorio;
            this.Editoras = editorasRepositorio;
            this.Autores = autoresRepositorio;
            this.Categorias = categoriasRepositorio;
        }
    }
}
