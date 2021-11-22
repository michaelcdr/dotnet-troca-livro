using MediatR;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterPacoteQuery : IRequest<ObterPacoteResultado>
    {
        public int PacoteId { get; set; }
        public ObterPacoteQuery( int pacoteId)
        {
            this.PacoteId = pacoteId;
        }
    }
}
