using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.CasosDeUsos.LogarUsuario;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly string erroLogar = "Não foi posivel autenticar.";
        private IMediator _mediator;

        public UsuarioController(IMediator mediator)
        {
            this._mediator = mediator;
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
                var logarResultado = await _mediator.Send(new LogarUsuarioCommand(model.Usuario, model.Senha));

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
    }
}
