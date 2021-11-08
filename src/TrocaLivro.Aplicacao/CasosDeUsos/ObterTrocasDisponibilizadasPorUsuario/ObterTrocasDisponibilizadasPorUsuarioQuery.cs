using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterTrocasDisponibilizadasPorUsuarioQuery : IRequest<AppResponse<ObterTrocasDisponibilizadasPorUsuarioResultado>>
    {
        public string Usuario { get; set; }
        public ObterTrocasDisponibilizadasPorUsuarioQuery(string usuario)
        {
            this.Usuario = usuario;
        }
    }
}
