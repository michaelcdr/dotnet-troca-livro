using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Infra.Services;

namespace TrocaLivro.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/pacotes")]
    public class PacoteController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IGerenciadorToken _gerenciadorToken;

        public PacoteController(IMediator mediator, IGerenciadorToken gerenciadorToken)
        {
            this._mediator = mediator;
            this._gerenciadorToken = gerenciadorToken;
        }

        /// <summary>
        /// Compra um pacote de pontos.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("Comprar"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Comprar(ComprarPacoteCommand comando)
        {
            string token = HttpContext.Request.Headers["Authorization"];
            comando.Usuario = _gerenciadorToken.ObterNomeUsuario(token);
            AppResponse<ComprarPacoteResultado> resposta = await _mediator.Send(comando);

            if (!resposta.Sucesso) return BadRequest(resposta);

            return Ok(resposta);
        }

        /// <summary>
        /// Compra um pacote de pontos.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet("Obter/{id}"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Obter(int id)
        {
            //string token = HttpContext.Request.Headers["Authorization"];
            //query.Usuario = _gerenciadorToken.ObterNomeUsuario(token);
            ObterPacoteResultado resposta = await _mediator.Send(new ObterPacoteQuery(id));

            return Ok(resposta);
        }
    }
}