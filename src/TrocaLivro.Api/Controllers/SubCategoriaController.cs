using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;

namespace TrocaLivro.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SubCategoriaController : Controller
    {
        private readonly IMediator _mediator;

        public SubCategoriaController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("ObterTodasDaCategoria/{idCategoria}")]
        public async Task<IActionResult> ObterTodasDaCategoria(int idCategoria)
        {
            return Ok(await _mediator.Send(new ObterSubCategoriasQuery(idCategoria)));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new ObterSubCategoriasQuery()));
        }
    }
}
