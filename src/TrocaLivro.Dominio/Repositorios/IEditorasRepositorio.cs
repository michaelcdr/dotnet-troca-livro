using System.Collections.Generic;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;

namespace TrocaLivro.Dominio.Repositorios
{
    public interface IEditorasRepositorio : IRepositorio<Editora>
    {
        Task<List<Editora>> ObterPorIds(List<int> editorasIds);
    }
}
