using TrocaLivro.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;

namespace TrocaLivro.Dominio.Repositorios
{
    public interface ILivrosRepositorio : IRepositorio<Livro>
    {
        Task<List<Livro>> PesquisarLivrosComAutores(Expression<Func<Livro, bool>> predicado);
        Task<List<Livro>> ObterLivrosComAutores();
        Task<Livro> Obter(int id);
    }
}
