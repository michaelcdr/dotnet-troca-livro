using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ComprarPacoteCommand : IRequest<AppResponse<ComprarPacoteResultado>>
    {
        public int PacoteId { get; set; }
        public string Usuario { get; set; }
        public ComprarPacoteCommand(int pacoteId)
        {
            this.PacoteId = pacoteId;   
        }
    }
}
