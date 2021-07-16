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
    }
}
