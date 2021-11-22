using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class MarcarLivroComoRecebidoCommand : IRequest<AppCommandResponse>
    {
        public int TrocaId { get; set; }
        public string UsuarioId { get; set; }
        public MarcarLivroComoRecebidoCommand(int trocaId)
        {
            this.TrocaId = trocaId;
        }
    }
}
