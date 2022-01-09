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
    [Route("api/v{version:apiVersion}/Manutencoes"), ApiExplorerSettings(IgnoreApi = true)]
    public class ManutencoesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IGerenciadorToken _gerenciadorToken;

        public ManutencoesController(IMediator mediator, IGerenciadorToken gerenciadorToken)
        {
            this._mediator = mediator;
            this._gerenciadorToken = gerenciadorToken;
        }

        [HttpPost("GerarUrlsAmigaveis"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GerarUrlsAmigaveis(GerarUrlsAmigaveisCommand comando)
        {
            comando.Usuario = _gerenciadorToken.ObterNomeUsuario(HttpContext.Request.Headers["Authorization"]);
            AppCommandResponse resposta = await _mediator.Send(comando);

            if (!resposta.Sucesso) return BadRequest(resposta);

            return Ok(resposta);
        }         
    }
}