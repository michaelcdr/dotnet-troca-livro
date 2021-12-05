using System.Collections.Generic;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Dominio.Repositorios
{
    public interface IAutoresRepositorio : IRepositorio<Autor>
    {
        Task<List<LivroAutor>> ObterParaLivros(List<int> livrosIds);
        Task<bool> Existe(string nome);
    }

}
