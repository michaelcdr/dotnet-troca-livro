using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Repositorios;
using TrocaLivro.Infra.Data;

namespace TrocaLivro.Infra.Repositorios.EF
{
    public class CategoriasRepositorio : Repositorio<Categoria>, ICategoriasRepositorio
    {
        public CategoriasRepositorio(ApplicationDbContext context) : base(context) { }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public async Task<IList<Categoria>> ObterTodas()
        {
            return await ApplicationDbContext.Categorias.OrderBy(e => e.Nome).ToListAsync();
        }

        public async Task<IList<SubCategoria>> ObterSubCategorias(int categoriaId)
        {
            return await ApplicationDbContext.SubCategorias
                .Include(subcategoria => subcategoria.Categoria)
                .Where(subcategoria => subcategoria.CategoriaId == categoriaId)
                .OrderBy(subcategoria => subcategoria.Nome).ToListAsync();
        }

        public async Task<IList<SubCategoria>> ObterTodasSubCategorias()
        {
            return await ApplicationDbContext.SubCategorias
                .Include(subcategoria => subcategoria.Categoria)
                .OrderBy(subcategoria => subcategoria.Nome).ToListAsync();
        }
    }
}
