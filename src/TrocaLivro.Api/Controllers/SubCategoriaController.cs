using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.DTO;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0"), ApiExplorerSettings(IgnoreApi = true)]
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

        /// <summary>
        /// Retorna todas subcategorias.
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterPorUrlAmigavel/{urlAmigavel}")]
        public async Task<IActionResult> ObterPorUrlAmigavel(string urlAmigavel)
        {
            AppResponse<SubCategoriaDTO> resultado = await _mediator.Send(new ObterSubCategoriaPorUrlAmigavelQuery(urlAmigavel));
            return Ok(resultado.Dados);
        }
    }
}
