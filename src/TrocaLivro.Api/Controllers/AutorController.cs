using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.CasosDeUsos.ObterTodosAutores;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Requests;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0"), ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AutorController : Controller
    {
        private readonly IMediator _mediator;

        public AutorController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            AppResponse<ObterTodosAutoresResultado> resposta = await _mediator.Send(new ObterTodosAutoresQuery());

            if (!resposta.Sucesso) return BadRequest(resposta.Erros);

            return Ok(resposta.Dados.Autores);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriarAutorCommand command)
        {
            AppResponse<CriarAutorResultado> resposta = await _mediator.Send(command); 

            if (!resposta.Sucesso) return BadRequest(resposta);

            var uri = Url.Action("Get", new { id = resposta.Dados.Id });

            return Created(uri, resposta);
        }
    }
}
