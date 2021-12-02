using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.DTO;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Infra.Data;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterSubCategoriaPorUrlAmigavelQueryHandler : IRequestHandler<ObterSubCategoriaPorUrlAmigavelQuery, AppResponse<SubCategoriaDTO>>
    {
        private readonly ApplicationDbContext context;

        public ObterSubCategoriaPorUrlAmigavelQueryHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<AppResponse<SubCategoriaDTO>> Handle(ObterSubCategoriaPorUrlAmigavelQuery request, CancellationToken cancellationToken)
        {
            SubCategoria subCategoria = await context.SubCategorias
                .Include(e => e.Categoria).AsNoTracking()
                .SingleOrDefaultAsync(subCategoriaAtual => subCategoriaAtual.UrlAmigavel == request.UrlAmigavel);

            if (subCategoria == null) 
                return new AppResponse<SubCategoriaDTO>("Nenhuma subcategoria encontrada.", false, new List<Dominio.Notificacao>()
                {
                    new Dominio.Notificacao("Nenhuma subcategoria encontrada.","")
                });

            return new AppResponse<SubCategoriaDTO>(true, "Subcategoria encontrada", new SubCategoriaDTO
            {
                CategoriaId = subCategoria.CategoriaId,
                Id = subCategoria.Id,
                Nome = subCategoria.Nome,
                NomeCategoria = subCategoria.Categoria.Nome,
                UrlAmigavel = subCategoria.UrlAmigavel
            });
        }
    }
}
