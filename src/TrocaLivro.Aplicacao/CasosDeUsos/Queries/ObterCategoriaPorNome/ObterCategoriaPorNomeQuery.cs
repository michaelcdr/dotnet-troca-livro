using MediatR;
using TrocaLivro.Aplicacao.DTO;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterCategoriaPorNomeQuery : IRequest<AppResponse<CategoriaDTO>>
    {
        public ObterCategoriaPorNomeQuery(string nome)
        {
            Nome = nome;
        }

        public string Nome { get; set; }

    }
}
