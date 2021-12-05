using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class CriarCategoriaCommand : IRequest<AppCommandResponse>
    {
        public string Nome { get; set; }
    } 
}
