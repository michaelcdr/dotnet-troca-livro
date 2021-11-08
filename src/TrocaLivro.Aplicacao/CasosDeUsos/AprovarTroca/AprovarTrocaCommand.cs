using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class AprovarTrocaCommand : IRequest<AppResponse<AprovarTrocaResultado>>
    {
        public AprovarTrocaCommand(int trocaId )
        { 
            TrocaId = trocaId;
        }

        public string Usuario { get; set; }
        public int TrocaId { get; set; }
    }
}
