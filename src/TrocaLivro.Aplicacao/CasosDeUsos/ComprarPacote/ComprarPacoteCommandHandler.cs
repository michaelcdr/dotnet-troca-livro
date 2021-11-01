using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Infra.Data;
using TrocaLivro.Infra.Repositorios.Data;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ComprarPacoteCommandHandler : IRequestHandler<ComprarPacoteCommand, AppResponse<ComprarPacoteResultado>>
    {
        private readonly ApplicationDbContext context;

        public ComprarPacoteCommandHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<AppResponse<ComprarPacoteResultado>> Handle(ComprarPacoteCommand request, CancellationToken cancellationToken)
        {
            Usuario usuario = await context.Usuarios.SingleAsync(e => e.UserName == request.Usuario);
            PacotePontos pacotePontos = new PacotesRepositorio().Obter(request.PacoteId);
            usuario.AdicionarPontos(pacotePontos.Pontos);

            await context.SaveChangesAsync();

            return new AppResponse<ComprarPacoteResultado>(true, "Pontos comprados com sucesso.");
        }
    }
}
