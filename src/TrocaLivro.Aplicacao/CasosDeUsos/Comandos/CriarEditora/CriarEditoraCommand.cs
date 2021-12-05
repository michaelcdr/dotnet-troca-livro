using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class CriarEditoraCommand : IRequest<AppResponse<CriarEditoraResultado>>
    {
        public string Nome { get; set; }
    }
}
