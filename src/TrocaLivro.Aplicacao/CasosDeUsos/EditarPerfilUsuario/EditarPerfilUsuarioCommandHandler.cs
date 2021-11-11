using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class EditarPerfilUsuarioCommandHandler : IRequestHandler<EditarPerfilUsuarioCommand, AppResponse<EditarPerfilUsuarioResultado>>
    {
        public async Task<AppResponse<EditarPerfilUsuarioResultado>> Handle(EditarPerfilUsuarioCommand request, CancellationToken cancellationToken)
        {
            return new AppResponse<EditarPerfilUsuarioResultado>();
        }
    }
}
