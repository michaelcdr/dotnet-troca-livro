using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Infra.Data;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class SolicitarTrocaCommandHandler : IRequestHandler<SolicitarTrocaCommand, AppResponse<SolicitarTrocaResultado>>
    {
        private readonly ApplicationDbContext db;

        public SolicitarTrocaCommandHandler(ApplicationDbContext context)
        {
            this.db = context;
        }

        public async Task<AppResponse<SolicitarTrocaResultado>> Handle(SolicitarTrocaCommand comando, CancellationToken cancellationToken)
        {
            Usuario usuario = await db.Usuarios.SingleAsync(e => e.UserName == comando.Usuario);
            Troca troca = await db.Trocas.SingleAsync(e => e.Id == comando.TrocaId);

            if (troca.Status != Dominio.Enums.StatusTroca.Disponibilizado)
                return new AppResponse<SolicitarTrocaResultado>(false, "Esse livro não está mais disponível para troca.");

            if (usuario.Pontos < troca.Pontos)
                return new AppResponse<SolicitarTrocaResultado>(false, "Você não possui pontos suficientes para efetuar essa troca.");

            troca.MarcarComoTrocaSolicitada(usuario.Id);

            await db.SaveChangesAsync();

            return new AppResponse<SolicitarTrocaResultado>();
        }
    }
}
