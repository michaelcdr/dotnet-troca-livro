using System.Threading.Tasks;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Infra.Services
{
    public interface IGeradorToken
    {
        Task<AppResponse<TokenResultado>> Gerar(string userName);
    }
}
