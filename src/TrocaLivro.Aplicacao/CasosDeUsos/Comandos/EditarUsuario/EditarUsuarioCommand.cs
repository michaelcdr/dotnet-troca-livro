using MediatR;
using Microsoft.AspNetCore.Http;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class EditarUsuarioCommand : IRequest<AppCommandResponse>
    {
        public string UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Sobrenome { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
