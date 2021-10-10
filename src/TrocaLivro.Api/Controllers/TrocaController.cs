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
    [Route("api/v{version:apiVersion}/trocas")]
    public class TrocaController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IGerenciadorToken _gerenciadorToken;

        public TrocaController(IMediator mediator, IGerenciadorToken gerenciadorToken)
        {
            this._mediator = mediator;
            this._gerenciadorToken = gerenciadorToken;
        }

        /// <summary>
        /// Disponibiliza um livro para troca.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("Disponibilizar"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Disponibilizar(DisponibilizarLivroParaTrocaCommand comando)
        {
            string token = HttpContext.Request.Headers["Authorization"];
            comando.Usuario = _gerenciadorToken.ObterNomeUsuario(token);
            AppResponse<DisponibilizarLivroParaTrocaResultado> resposta = await _mediator.Send(comando);

            if (!resposta.Sucesso) return BadRequest(resposta.Erros);

            return Ok(resposta);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] CriarEditoraCommand command)
        //{
        //    AppResponse<CriarEditoraResultado> resposta = await _mediator.Send(command);

        //    if (!resposta.Sucesso) return BadRequest(resposta);

        //    var uri = Url.Action("Get", new { id = resposta.Dados.Id });

        //    return Created(uri, resposta);
        //}
    }
}
