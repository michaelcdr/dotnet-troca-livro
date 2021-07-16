using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Repositorios;
using TrocaLivro.Infra.Data;

namespace TrocaLivro.Infra.Repositorios.EF
{
    public class LivrosRepositorio : Repositorio<Livro>, ILivrosRepositorio
    {
        public LivrosRepositorio(ApplicationDbContext context) : base(context) {}

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public async Task<Livro> Obter(int id)
        {
            return await ApplicationDbContext.Livros
                .Where(e => e.Id == id).Include(e => e.Autores).ThenInclude(e => e.Autor)
                .Include(e => e.Editora)
                .Include(e => e.Imagens)
                .Include(e => e.Arquivos)
                .Include(e => e.Categoria)
                .SingleAsync();
        }

        public async Task<List<Livro>> ObterLivrosComAutores()
        {
            return await ApplicationDbContext.Livros
                .Include(e => e.Autores).ThenInclude(e => e.Autor)
                .Include(e => e.Imagens)
                .Include(e => e.Editora).OrderByDescending(e => e.DataCadastro).ToListAsync();
        }

        public async Task<List<Livro>> PesquisarLivrosComAutores(Expression<Func<Livro, bool>> predicado)
        {
            return await ApplicationDbContext.Livros.Include(e => e.Autores).ThenInclude(e => e.Autor)
                .Include(e => e.Editora)
                .Where(predicado).OrderByDescending(e => e.DataCadastro)
                .ToListAsync();
        }
    }
}
