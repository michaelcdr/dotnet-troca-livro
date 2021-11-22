using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Infra.Data;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class ObterPontosQueryHandler : IRequestHandler<ObterPontosQuery, AppResponse<ObterPontosResultado>>
    {
        private readonly ApplicationDbContext context;

        public ObterPontosQueryHandler(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<AppResponse<ObterPontosResultado>> Handle(ObterPontosQuery request, CancellationToken cancellationToken)
        {
            var pontos = await context.Usuarios.Where(usuarioAtual => usuarioAtual.UserName == request.Usuario)
                .Select(usuarioAtual => usuarioAtual.Pontos)
                .SingleAsync();

            var resposta = new ObterPontosResultado(pontos);

            return new AppResponse<ObterPontosResultado>(true, "Pontos obtidos com sucesso", resposta);    
        }
    }
}
