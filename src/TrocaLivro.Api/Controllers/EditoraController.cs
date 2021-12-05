using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0"), ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/v{version:apiVersion}/editoras")]
    public class EditoraController : Controller
    {
        private readonly IMediator _mediator;

        public EditoraController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            AppResponse<ObterTodasEditorasResultado> resposta = await _mediator.Send(new ObterTodasEditorasQuery());

            if (!resposta.Sucesso) return BadRequest(resposta.Erros);

            return Ok(resposta.Dados.Editoras);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriarEditoraCommand command)
        {
            AppResponse<CriarEditoraResultado> resposta = await _mediator.Send(command);

            if (!resposta.Sucesso) return BadRequest(resposta);

            var uri = Url.Action("Get", new { id = resposta.Dados.Id });

            return Created(uri, resposta);
        }
    }
}
