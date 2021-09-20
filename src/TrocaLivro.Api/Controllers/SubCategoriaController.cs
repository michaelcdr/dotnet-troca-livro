using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;

namespace TrocaLivro.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/subcategorias")]
    public class SubCategoriaController : Controller
    {
        private readonly IMediator _mediator;

        public SubCategoriaController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// Retorna todas subcategorias de uma categoria especifica.
        /// </summary>
        /// <param name="idCategoria"></param>
        /// <returns></returns>
        [HttpGet("ObterTodasDaCategoria/{idCategoria}")]
        public async Task<IActionResult> ObterTodasDaCategoria(int idCategoria)
        {
            ObterSubCategoriasResultado resultado = await _mediator.Send(new ObterSubCategoriasQuery(idCategoria));
            return Ok(resultado.SubCategorias);
        }

        /// <summary>
        /// Retorna todas subcategorias.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ObterSubCategoriasResultado resultado = await _mediator.Send(new ObterSubCategoriasQuery());
            return Ok(resultado.SubCategorias);
        }
    }
}
