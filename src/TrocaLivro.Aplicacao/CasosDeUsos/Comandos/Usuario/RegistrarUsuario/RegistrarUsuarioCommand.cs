using MediatR;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class RegistrarUsuarioCommand: IRequest<AppResponse<RegistrarUsuarioResultado>>
    {
        public string Nome { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string ConfirmarSenha { get; set; }
        public string Sobrenome { get; set; }
    }
}
