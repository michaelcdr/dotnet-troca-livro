using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class CriarSubCategoriaCommand : IRequest<AppCommandResponse>
    {
        public string Nome { get; set; }
        public int? CategoriaId { get; set; }
    }
}