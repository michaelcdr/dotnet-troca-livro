using System.Security.Claims;

namespace TrocaLivro.Aplicacao.HttpClients
{
    public interface IAtualizadorToken
    {
        void AtualizarToken(Claim claimComToken);
    }
}