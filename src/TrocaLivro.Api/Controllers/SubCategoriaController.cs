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

        [HttpGet("{categoriaId}")]
        public async Task<IActionResult> Get(int categoriaId)
        {
            ObterSubCategoriasResultado resultado = await _mediator.Send(new ObterSubCategoriasQuery(categoriaId));
            return Ok(resultado.SubCategorias);
        }
    }
}
