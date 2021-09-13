using MediatR;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterSubCategoriasQuery : IRequest<ObterSubCategoriasResultado>
    {
        public int CategoriaId { get;  set; }
    }
}
