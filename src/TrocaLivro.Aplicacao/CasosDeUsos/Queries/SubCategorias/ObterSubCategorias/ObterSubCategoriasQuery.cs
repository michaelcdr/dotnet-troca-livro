using MediatR;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterSubCategoriasQuery : IRequest<ObterSubCategoriasResultado>
    {
        public ObterSubCategoriasQuery(int? categoriaId = null)
        {
            CategoriaId = categoriaId;
        }

        public int? CategoriaId { get;  set; }
    }
}
