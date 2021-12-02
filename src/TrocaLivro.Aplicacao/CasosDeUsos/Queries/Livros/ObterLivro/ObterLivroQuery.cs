using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterLivroQuery : IRequest<AppResponse<ObterLivroResultado>>
    {
        public ObterLivroQuery(int livroId)
        {
            LivroId = livroId;
        }

        public int LivroId { get; set; }
    }
}