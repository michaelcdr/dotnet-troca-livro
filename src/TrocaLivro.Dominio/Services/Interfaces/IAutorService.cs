using System.Collections.Generic;
using System.Threading.Tasks;
using TrocaLivro.Dominio.DTO;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Requests;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Dominio.Services
{
    public interface IAutorService
    {
        Task<AppResponse<Autor>> Criar(AutorRequest request);
        Task<AppResponse<IList<AutorDTO>>> ObterTodas();
    }
}
