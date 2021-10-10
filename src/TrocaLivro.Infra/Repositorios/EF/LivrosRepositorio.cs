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
                .Include(e => e.DiponibilizacaoParaTrocas).ThenInclude(e => e.UsuarioQueDisponibilizouParaTroca)
                .SingleAsync();
        }

        public void Atualizar(Livro livro)
        {
            ApplicationDbContext.Livros.Update(livro);
        }

        public void RemoverImagens(List<int> idsImagens)
        {
            ApplicationDbContext.Imagens.RemoveRange(ApplicationDbContext.Imagens.Where(e => idsImagens.Contains(e.Id)));
        }

        public async Task<List<int>> ObterIdsImagens(int id)
            => await ApplicationDbContext.Imagens.AsNoTracking()
                                         .Where(e => e.LivroId == id).Select(e => e.Id).ToListAsync();

        public async Task<List<Livro>> ObterLivrosComAutores(int tamanhoPagina, int qtdRegistrosAPular,  string termoPesquisa)
        {
            return await ApplicationDbContext.Livros
                .Where(e => termoPesquisa != null && termoPesquisa != string.Empty 
                    ?   e.Titulo.Contains(termoPesquisa) || e.Descricao.Contains(termoPesquisa) || 
                        e.Subtitulo.Contains(termoPesquisa)
                    : true)
                .Where(e => !e.Deletado)
                .OrderByDescending(e => e.DataCadastro).Take(tamanhoPagina).ToListAsync();
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

        public async Task<List<Imagem>> ObterImagens(List<int> livrosIds)
        {
            return await ApplicationDbContext.Imagens.Where(imagemAtual => livrosIds.Contains(imagemAtual.LivroId)).ToListAsync();
        }

        public async Task DisponibilizarParaTroca(LivroDisponibilizadoParaTroca disponibilizacao)
        {
            await ApplicationDbContext.LivrosDisponibilizadosParaTrocas.AddAsync(disponibilizacao);
        }
    }
}