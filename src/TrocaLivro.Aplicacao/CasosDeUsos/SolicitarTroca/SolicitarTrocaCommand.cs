using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class SolicitarTrocaCommand : IRequest<AppResponse<SolicitarTrocaResultado>>
    {
        public int TrocaId { get; set; }
        public string Usuario { get; set; }
        public SolicitarTrocaCommand()
        {

        }
        public SolicitarTrocaCommand(int trocaId)
        {
            TrocaId = trocaId;
        }
    }
}