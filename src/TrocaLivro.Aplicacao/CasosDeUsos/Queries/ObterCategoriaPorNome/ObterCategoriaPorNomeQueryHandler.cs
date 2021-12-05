using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.DTO;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Dominio.Transacoes;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterCategoriaPorNomeQueryHandler : IRequestHandler<ObterCategoriaPorNomeQuery, AppResponse<CategoriaDTO>>
    {
        private readonly IUnitOfWork unitOfWork;

        public ObterCategoriaPorNomeQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        } 

        public async Task<AppResponse<CategoriaDTO>> Handle(ObterCategoriaPorNomeQuery request, CancellationToken cancellationToken)
        {
            Categoria categoria = await unitOfWork.Categorias.ObterPorNome(request.Nome);

            var categoriaDTO = new CategoriaDTO(categoria.Id, categoria.Nome);

            return new AppResponse<CategoriaDTO>(true, "Categoria obtida com sucesso.", categoriaDTO);
        }
    }
}
