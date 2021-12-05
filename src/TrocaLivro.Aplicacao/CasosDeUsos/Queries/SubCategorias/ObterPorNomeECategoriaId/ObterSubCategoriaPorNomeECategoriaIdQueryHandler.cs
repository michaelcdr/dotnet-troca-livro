using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.DTO;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Infra.Data;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterSubCategoriaPorNomeECategoriaIdQueryHandler : IRequestHandler<ObterSubCategoriaPorNomeECategoriaIdQuery, AppResponse<SubCategoriaDTO>>
    {
        private readonly ApplicationDbContext context;

        public ObterSubCategoriaPorNomeECategoriaIdQueryHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<AppResponse<SubCategoriaDTO>> Handle(ObterSubCategoriaPorNomeECategoriaIdQuery request, CancellationToken cancellationToken)
        {
            SubCategoria subCategoria = await context.SubCategorias
                .SingleOrDefaultAsync(e => e.CategoriaId == request.CategoriaId && e.Nome == request.NomeSubCategoria);

            if (subCategoria == null)
                return new AppResponse<SubCategoriaDTO>(true, "Subcategoria obtida com sucesso", null);

            var subCategoriaDTO = new SubCategoriaDTO()
            {
                Id = subCategoria.Id,
                Nome = subCategoria.Nome,
                CategoriaId = subCategoria.CategoriaId
            };

            return new AppResponse<SubCategoriaDTO>(true, "Subcategoria obtida com sucesso", subCategoriaDTO);
        }
    }
}