using System.Threading.Tasks;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Infra.Services
{
    public interface IGerenciadorToken
    {
        Task<AppResponse<TokenResultado>> Gerar(string userName);
        string ObterNomeUsuario(string token);
    }
}
