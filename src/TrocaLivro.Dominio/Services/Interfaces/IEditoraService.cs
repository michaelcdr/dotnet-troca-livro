using System.Collections.Generic;
using System.Threading.Tasks;
using TrocaLivro.Dominio.DTO;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Requests;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Dominio.Services
{
    public interface IEditoraService
    {
        Task<AppResponse<Editora>> Criar(EditoraRequest request);
        Task<AppResponse<IList<EditoraDTO>>> ObterTodas();
    }
}
