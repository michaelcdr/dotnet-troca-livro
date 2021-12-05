using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class GerarUrlsAmigaveisCommand : IRequest<AppCommandResponse>
    {
        public string Usuario { get; set; }
    }
}
