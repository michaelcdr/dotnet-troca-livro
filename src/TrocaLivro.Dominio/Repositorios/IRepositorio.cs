using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TrocaLivro.Dominio.Repositorios
{
    public interface IRepositorio<TEntity> where TEntity : class
    {
        void Add(TEntity entidade);
        Task<TEntity> Get(object id);
        void Delete(TEntity entidade);
        Task<IList<TEntity>> ObterTodos();
        Task<IList<TEntity>> Pesquisar(Expression<Func<TEntity, bool>> predicado);
    }
}
