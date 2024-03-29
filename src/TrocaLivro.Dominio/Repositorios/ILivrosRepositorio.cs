﻿using TrocaLivro.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;

namespace TrocaLivro.Dominio.Repositorios
{
    public interface ILivrosRepositorio : IRepositorio<Livro>
    {
        Task<List<Livro>> PesquisarLivrosComAutores(Expression<Func<Livro, bool>> predicado);
        Task<List<Livro>> ObterLivrosComAutores(int tamanhoPagina, int quantidadeRegistrosAPular, string termoPesquisa = null, string subCategoriaUrlAmigavel = null);
        Task<Livro> Obter(int id);
        Task<int> ObterTotal();
        Task<int> ObterTotalDeTrocas();
        Task<int> ObterTotalLivrosDisponiveisParaTroca();
        Task<bool> VerificarExistencia(string iSBN, int? idLivroAtual=null);
        Task<List<int>> ObterIdsImagens(int id);
        void RemoverImagens(List<int> idsImagens);
        Task<List<Avaliacao>> ObterAvaliacoes(int livroId);
        void Atualizar(Livro livro);
        Task<List<Imagem>> ObterImagens(List<int> livrosIds);
        Task Avaliar(Avaliacao avaliacao);
        Task DisponibilizarParaTroca(Troca disponibilizacao);
    }
}
