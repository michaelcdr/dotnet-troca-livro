using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterTrocaQuery : IRequest<AppResponse<ObterTrocaResultado>>
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
    }
}
