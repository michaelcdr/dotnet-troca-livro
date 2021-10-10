using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class DisponibilizarLivroParaTrocaCommand : IRequest<AppResponse<DisponibilizarLivroParaTrocaResultado>>
    {
        public int LivroId { get; set; }
        public string Descritivo { get; set; }
        public int Pontos { get; set; }
        public string Usuario { get; set; }
    }
}