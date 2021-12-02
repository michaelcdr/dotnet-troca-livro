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

        public ApplicationDbContext Contexto
        {
            get { return Context as ApplicationDbContext; }
        }

        public async Task<Livro> Obter(int id)
        {
            return await Contexto.Livros
                .Where(e => e.Id == id && !e.Deletado).Include(e => e.Autores).ThenInclude(e => e.Autor)
                .Include(e => e.Editora)
                .Include(e => e.Imagens)
                .Include(e => e.Arquivos)
                .Include(e => e.SubCategoria)
                .Include(e => e.Avaliacoes).ThenInclude(e => e.Usuario)
                .Include(e => e.DiponibilizacaoParaTrocas).ThenInclude(e => e.UsuarioQueDisponibilizouParaTroca)
                .SingleAsync();
        }

        public void Atualizar(Livro livro)
        {
            Contexto.Livros.Update(livro);
        }

        public void RemoverImagens(List<int> idsImagens)
        {
            Contexto.Imagens.RemoveRange(Contexto.Imagens.Where(e => idsImagens.Contains(e.Id)));
        }

        public async Task<List<int>> ObterIdsImagens(int id)
            => await Contexto.Imagens.AsNoTracking()
                                         .Where(e => e.LivroId == id).Select(e => e.Id).ToListAsync();

        public async Task<List<Livro>> ObterLivrosComAutores(
            int tamanhoPagina, int qtdRegistrosAPular, string termoPesquisa, string subCategoriaUrlAmigavel)
        {
            return await Contexto.Livros
                .Where(e => termoPesquisa != null && termoPesquisa != string.Empty
                    ? e.Titulo.Contains(termoPesquisa) || e.Descricao.Contains(termoPesquisa) ||
                        e.Subtitulo.Contains(termoPesquisa) ||
                        e.Autores.Any(autor => autor.Autor.Nome.Contains(termoPesquisa))
                    : true)
                .Where(e => !e.Deletado)
                .Where(e => subCategoriaUrlAmigavel != null ? e.SubCategoria.UrlAmigavel == subCategoriaUrlAmigavel : true)
                .Include(e => e.Autores).ThenInclude(a => a.Autor)
                .OrderByDescending(e => e.DataCadastro).Take(tamanhoPagina).ToListAsync();
        }

        public async Task<int> ObterTotal()
        {
            return await Contexto.Livros.AsNoTracking().CountAsync(e => !e.Deletado);
        }

        public async Task<int> ObterTotalDeTrocas()
        {
            return await Contexto.Trocas.AsNoTracking()
                .Where(e => e.Status == Dominio.Enums.StatusTroca.LivroEnviado)
                .Select(e => e.Id)
                .CountAsync();
        }

        public async Task<int> ObterTotalLivrosDisponiveisParaTroca()
        {
            return await Contexto.Trocas.AsNoTracking()
               .Where(e => e.Status == Dominio.Enums.StatusTroca.Disponibilizado)
               .Select(e => e.Id)
               .CountAsync();
        }

        public async Task<List<Livro>> PesquisarLivrosComAutores(Expression<Func<Livro, bool>> predicado)
        {
            return await Contexto.Livros.Include(e => e.Autores).ThenInclude(e => e.Autor)
                .Include(e => e.Editora)
                .Where(predicado).OrderByDescending(e => e.DataCadastro)
                .ToListAsync();
        }

        public async Task<bool> VerificarExistencia(string iSBN, int? idLivroAtual = null)
        {
            if (idLivroAtual != null)
                return await Contexto.Livros.AnyAsync(livro => livro.ISBN == iSBN && livro.Id != (int)idLivroAtual);
            else
                return await Contexto.Livros.AnyAsync(livro => livro.ISBN == iSBN);
        }

        public async Task<List<Imagem>> ObterImagens(List<int> livrosIds)
        {
            return await Contexto.Imagens.Where(imagemAtual => livrosIds.Contains(imagemAtual.LivroId)).ToListAsync();
        }

        public async Task DisponibilizarParaTroca(Troca disponibilizacao)
        {
            await Contexto.Trocas.AddAsync(disponibilizacao);
        }

        public async Task Avaliar(Avaliacao avaliacao)
        {
            await Contexto.Avaliacoes.AddAsync(avaliacao);
        }
         
        public async Task<List<Avaliacao>> ObterAvaliacoes(int livroId)
        {
            return await Contexto.Avaliacoes.Where(e => e.LivroId == livroId)
                .Include(a => a.Usuario)
                .OrderByDescending(e => e.AvaliadoEm)
                .ToListAsync();
        }
    }
}