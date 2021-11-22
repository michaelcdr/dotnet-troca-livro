using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos.CriarEditora
{
    public class CriarEditoraCommand : IRequest<AppResponse<CriarEditoraResultado>>
    {
        public string Nome { get; set; }
    }
}
