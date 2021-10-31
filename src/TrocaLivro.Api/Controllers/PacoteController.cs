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

            if (!resposta.Sucesso) return BadRequest(resposta.Erros);

            return Ok(resposta);
        } 
    }
}