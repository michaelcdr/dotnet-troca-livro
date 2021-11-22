using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.Helpers;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.CasosDeUsos
{
    public class RegistrarUsuarioCommandHandler : IRequestHandler<RegistrarUsuarioCommand, AppResponse<RegistrarUsuarioResultado>>
    {
        private readonly UserManager<Usuario> _userManager;
        private const string MSG_ERRO = "Não foi possivel cadastrar o usuário.";
        private const string MSG_SUCESSO = "Usuário cadastrado com sucesso.";
        private const string ROLE_COMUM = "comum";
        public RegistrarUsuarioCommandHandler(UserManager<Usuario> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AppResponse<RegistrarUsuarioResultado>> Handle(RegistrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = new Usuario(request.Nome, request.Usuario, request.Email, request.Sobrenome);

            if (!usuario.TaValido()) return new AppResponse<RegistrarUsuarioResultado>("Dados inválidos", false, usuario.ObterErros());

            IdentityResult result = await _userManager.CreateAsync(usuario, request.Senha);

            if (!result.Succeeded)
                return new AppResponse<RegistrarUsuarioResultado>(MSG_ERRO, false, IdentityHelper.ObterErros(result));
            else
            {
                IdentityResult resultRole = await _userManager.AddToRoleAsync(usuario, ROLE_COMUM);

                if (!resultRole.Succeeded) return new AppResponse<RegistrarUsuarioResultado>(MSG_ERRO, false, IdentityHelper.ObterErros(result));
            }
            return new AppResponse<RegistrarUsuarioResultado>(true, MSG_SUCESSO, new RegistrarUsuarioResultado { });
        }
    }
}