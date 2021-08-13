using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TrocaLivro.Api.InputModel;
using TrocaLivro.Api.Interfaces;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : Controller
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly IGeradorToken _geradorToken;

        public ContaController(SignInManager<Usuario> signInManager, UserManager<Usuario> userManager, IGeradorToken geradorToken)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._geradorToken = geradorToken;
        }

        /// <summary>
        /// Gera um token para usar na autenticação da API
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Autenticar")]
        public async Task<IActionResult> Autenticar(AutenticacaoModel model)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = await _userManager.FindByNameAsync(model.Login);

                if (usuario == null) return BadRequest(new AppResponse<object>(false, "Usuário não encontrado."));

                if (!usuario.TaValido())
                    return BadRequest();
                else
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(usuario, model.Password, false);
                    var roles = await _userManager.GetRolesAsync(usuario);
                    string tipoUsuario = roles.First();

                    if (!result.Succeeded)
                        return BadRequest(new AppResponse<object>(false, "Não foi possível gerar o token."));
                    else
                    {
                        string tokenString = await _geradorToken.Gerar(model.Login, usuario);

                        return Ok(new AppResponse<object>(true, "Logado com sucesso. ", new
                        {
                            Token = tokenString,
                            Id = usuario.Id,
                            UserName = usuario.UserName,
                            Nome = usuario.Nome,
                            Email = usuario.Email,
                            Tipo = tipoUsuario
                        }));
                    }
                }
            }
            return BadRequest(new AppResponse<object>(false, "Não foi possível gerar o token."));
        }

        /// <summary>
        /// Registra um novo usuário
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Registrar")]
        public async Task<IActionResult> Registrar(RegistrarModel model)
        {
            return Ok();
        }
    }
}
