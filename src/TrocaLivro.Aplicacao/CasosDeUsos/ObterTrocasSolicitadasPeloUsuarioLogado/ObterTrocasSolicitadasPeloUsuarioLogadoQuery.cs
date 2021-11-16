using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterTrocasSolicitadasPeloUsuarioLogadoQuery : IRequest<AppResponse<ObterTrocasSolicitadasPeloUsuarioLogadoResultado>>
    {
        public string UsuarioQueSolicitouTroca { get;  set; }
        public ObterTrocasSolicitadasPeloUsuarioLogadoQuery(string usuario)
        {
            this.UsuarioQueSolicitouTroca = usuario;    
        }
    }
}
