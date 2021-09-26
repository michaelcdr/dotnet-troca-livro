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
                .Where(e => e.Id == id && !e.Deletado).Include(e => e.Autores).ThenInclude(e => e.Autor)
                .Include(e => e.Editora)
                .Include(e => e.Imagens)
                .Include(e => e.Arquivos)
                .Include(e => e.SubCategoria)
                .SingleAsync();
        }

        public async Task<List<Livro>> ObterLivrosComAutores()
        {
            return await ApplicationDbContext.Livros
                .Include(e => e.Autores).ThenInclude(e => e.Autor)
                .Include(e => e.Imagens).Include(e => e.Editora)
                .Where(e => !e.Deletado).OrderByDescending(e => e.DataCadastro).ToListAsync();
        }

        public async Task<int> ObterTotal()
        {
            return await ApplicationDbContext.Livros.AsNoTracking().CountAsync(e => !e.Deletado);
        }

        public async Task<int> ObterTotalDeTrocas()
        {
            return await ApplicationDbContext.LivrosDisponibilizadosParaTrocas.AsNoTracking()
                .Where(e => e.Status == Dominio.Enums.StatusTroca.TrocaConcluida)
                .Select(e => e.Id)
                .CountAsync();
        }

        public async Task<int> ObterTotalLivrosDisponiveisParaTroca()
        {
            return await ApplicationDbContext.LivrosDisponibilizadosParaTrocas.AsNoTracking()
               .Where(e => e.Status == Dominio.Enums.StatusTroca.Disponibilizado)
               .Select(e => e.Id)
               .CountAsync();
        }

        public async Task<List<Livro>> PesquisarLivrosComAutores(Expression<Func<Livro, bool>> predicado)
        {
            return await ApplicationDbContext.Livros.Include(e => e.Autores).ThenInclude(e => e.Autor)
                .Include(e => e.Editora)
                .Where(predicado).OrderByDescending(e => e.DataCadastro)
                .ToListAsync();
        }

        public async Task<bool> VerificarExistencia(string iSBN, int? idLivroAtual = null)
        {
            if (idLivroAtual != null)
                return await ApplicationDbContext.Livros.AnyAsync(livro => livro.ISBN == iSBN && livro.Id != (int)idLivroAtual);
            else
                return await ApplicationDbContext.Livros.AnyAsync(livro => livro.ISBN == iSBN);
        }
    }
}
