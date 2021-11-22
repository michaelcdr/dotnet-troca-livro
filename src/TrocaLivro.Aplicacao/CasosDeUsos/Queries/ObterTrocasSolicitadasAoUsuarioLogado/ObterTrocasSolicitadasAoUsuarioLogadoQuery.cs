using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterTrocasSolicitadasAoUsuarioLogadoQuery:IRequest<AppResponse<ObterTrocasSolicitadasAoUsuarioLogadoResultado>>
    {
        public ObterTrocasSolicitadasAoUsuarioLogadoQuery(string usuarioQueDisponibilizouTroca)
        {
            UsuarioQueDisponibilizouTroca = usuarioQueDisponibilizouTroca;
        }

        public string UsuarioQueDisponibilizouTroca { get; set; }
    }
}
