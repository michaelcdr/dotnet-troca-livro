using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Infra.Data;

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
            return new AppResponse<ComprarPacoteResultado>();
        }
    }
}
