using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterTrocasSolicitadasQuery:IRequest<AppResponse<ObterTrocasSolicitadasResultado>>
    {
        public ObterTrocasSolicitadasQuery(string usuarioQueDisponibilizouTroca)
        {
            UsuarioQueDisponibilizouTroca = usuarioQueDisponibilizouTroca;
        }

        public string UsuarioQueDisponibilizouTroca { get; set; }
    }
}
