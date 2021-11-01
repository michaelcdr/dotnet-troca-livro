using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.CasosDeUsos.LogarUsuario;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Infra.Services;

namespace TrocaLivro.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Usuario")]
    public class UsuarioController : Controller
    {
        private readonly string erroLogar = "Não foi posivel autenticar.";
        private readonly IMediator _mediator;
        private readonly IGerenciadorToken _gerenciadorToken;
        public UsuarioController(IMediator mediator, IGerenciadorToken gerenciadorToken)
        {
            this._mediator = mediator;
            this._gerenciadorToken = gerenciadorToken;
        }

        /// <summary>
        /// Gera um token para usar na autenticação da API
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Logar")]
        public async Task<IActionResult> Logar(LogarUsuarioModel model)
        {
            if (ModelState.IsValid)
            {
                AppResponse<LogarUsuarioResultado> logarResultado = await _mediator.Send(new LogarUsuarioCommand(model.Usuario, model.Senha));

                if (!logarResultado.Sucesso)
                    return BadRequest(new AppResponse<object>(false, logarResultado.Mensagem));
                
                return Ok(new AppResponse<LogarUsuarioResultado>(true, "Logado com sucesso. ", logarResultado.Dados));
            }
            return BadRequest(new AppResponse<object>(false, erroLogar));
        }

        /// <summary>
        /// Registra um novo usuário
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Registrar")]
        public async Task<IActionResult> Registrar(RegistrarUsuarioCommand model)
        {
            AppResponse<RegistrarUsuarioResultado> resposta = await _mediator.Send(model);
            return Ok(resposta);
        }

        /// <summary>
        /// Obtem pontos do usuário logado
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("Pontos"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Pontos()
        {
            var query = new ObterPontosQuery(_gerenciadorToken.ObterNomeUsuario(HttpContext.Request.Headers["Authorization"]));
            AppResponse<ObterPontosResultado> resposta = await _mediator.Send(query);
            return Ok(resposta);
        }

        /// <summary>
        /// Obtem um usuário pelo Username
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("{userName}"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get(string userName)
        {
            var query = new ObterUsuarioQuery(userName);
            AppResponse<ObterUsuarioResultado> resposta = await _mediator.Send(query);
            return Ok(resposta);
        }
    }
}
