using System.Collections.Generic;
using System.Threading.Tasks;
using TrocaLivro.Dominio.DTO;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Requests;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Dominio.Services
{
    public interface ILivroService
    {
        Task<AppResponse<LivroDTO>> Criar(LivroRequest request);
        Task<AppResponse<IList<LivroDTO>>> ObterTodos(ObterTodosLivrosRequest request);
        Task<AppResponse<LivroDTO>> Obter(int livroId);
    }
}
