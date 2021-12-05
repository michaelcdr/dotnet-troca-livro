using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Repositorios;
using TrocaLivro.Infra.Data;

namespace TrocaLivro.Infra.Repositorios.EF
{
    public class AutoresRepositorio : Repositorio<Autor>, IAutoresRepositorio
    {
        public AutoresRepositorio(ApplicationDbContext context) : base(context) { }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public async Task<bool> Existe(string nome)
        {
            return await ApplicationDbContext.Autores.AnyAsync(
                autorAtual => autorAtual.Nome == nome);
        }

        public async Task<List<LivroAutor>> ObterParaLivros(List<int> livrosIds)
        {
            return await ApplicationDbContext.LivrosAutores.Where(e => livrosIds.Contains(e.LivroId)).Include(e=>e.Autor).ToListAsync();
        }
    }
}
