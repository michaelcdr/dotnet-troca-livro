using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class MarcarLivroComoEnviadoCommand : IRequest<AppCommandResponse>
    {
        public MarcarLivroComoEnviadoCommand(int trocaId)
        {
            TrocaId = trocaId;
        }

        public int TrocaId { get; set; }
        public string Usuario { get; set; }
    }
}
