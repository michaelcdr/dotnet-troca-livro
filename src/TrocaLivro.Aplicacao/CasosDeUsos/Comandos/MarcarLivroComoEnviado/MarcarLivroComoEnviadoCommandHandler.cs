using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Infra.Data;

namespace TrocaLivro.Aplicacao.CasosDeUsos.MarcarLivroComoEnviado
{
    public class MarcarLivroComoEnviadoCommandHandler : IRequestHandler<MarcarLivroComoEnviadoCommand, AppCommandResponse>
    {
        private readonly ApplicationDbContext _db;

        public MarcarLivroComoEnviadoCommandHandler(ApplicationDbContext context)
        {
            this._db = context;
        }

        public async Task<AppCommandResponse> Handle(MarcarLivroComoEnviadoCommand request, CancellationToken cancellationToken)
        {
            Troca troca = await _db.Trocas.SingleAsync(trocaAtual => trocaAtual.Id == request.TrocaId);
            troca.MarcarComoEnviado();
            await _db.SaveChangesAsync();
            
            return new AppCommandResponse(true,"Livro enviado com sucesso.");
        }
    }
}
