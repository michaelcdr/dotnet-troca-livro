using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Repositorios;
using TrocaLivro.Infra.Data;

namespace TrocaLivro.Infra.Repositorios.EF
{
    public class EditorasRepositorio : Repositorio<Editora>, IEditorasRepositorio
    {
        public EditorasRepositorio(ApplicationDbContext context) : base(context) { }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public async Task<List<Editora>> ObterPorIds(List<int> editorasIds)
            => await ApplicationDbContext.Editoras.Where(editora => editorasIds.Contains(editora.Id)).ToListAsync();
    }
}