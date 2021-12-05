using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Infra.Data;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class AprovarTrocaCommandHandler : IRequestHandler<AprovarTrocaCommand, AppResponse<AprovarTrocaResultado>>
    {
        private readonly ApplicationDbContext _db;

        public AprovarTrocaCommandHandler(ApplicationDbContext context)
        {
            this._db = context;
        }

        public async Task<AppResponse<AprovarTrocaResultado>> Handle(AprovarTrocaCommand request, CancellationToken cancellationToken)
        {
            Troca troca = await _db.Trocas.SingleAsync(trocaAtual => trocaAtual.Id == request.TrocaId);

            troca.Aprovar();

            await _db.SaveChangesAsync();

            return new AppResponse<AprovarTrocaResultado>(true,"Troca aprovada com sucesso", new AprovarTrocaResultado());
        }
    }
}
