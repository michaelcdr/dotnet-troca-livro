using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Infra.Data;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class GerarUrlsAmigaveisCommandHandler : IRequestHandler<GerarUrlsAmigaveisCommand, AppCommandResponse>
    {
        private readonly ApplicationDbContext context;

        public GerarUrlsAmigaveisCommandHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<AppCommandResponse> Handle(GerarUrlsAmigaveisCommand request, CancellationToken cancellationToken)
        {
            List<SubCategoria> subCategorias = await context.SubCategorias.ToListAsync();

            foreach (SubCategoria subCategoria in subCategorias)
                subCategoria.GerarUrlAmigavel();

            await context.SaveChangesAsync();
            return new AppCommandResponse(true, "Urls geradas com sucesso.");
        }
    }
}