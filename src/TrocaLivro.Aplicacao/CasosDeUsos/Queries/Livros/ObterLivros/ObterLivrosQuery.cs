using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterLivrosQuery : IRequest<AppResponse<ObterLivrosResultado>>
    {
        public int TamanhoPagina { get; set; }
        public int QuantidadeRegistrosAPular { get; set; }
        public string TermoPesquisa { get; set; }
        public string SubCategoria { get; set; }
    }
}
