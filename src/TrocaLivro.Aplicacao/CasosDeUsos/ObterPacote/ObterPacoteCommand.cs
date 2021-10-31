using MediatR;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterPacoteCommand : IRequest<ObterPacoteResultado>
    {
        public int PacoteId { get; set; }
    }
}
