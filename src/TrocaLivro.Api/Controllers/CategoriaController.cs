using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.DTO;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Transacoes;

namespace TrocaLivro.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0"), ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CategoriaController : Controller
    {
        private IUnitOfWork _uow;
        public CategoriaController(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IList<Categoria> categorias = await _uow.Categorias.ObterTodas();

            List<CategoriaDTO> categoriaDTOs = categorias == null 
                ? new List<CategoriaDTO>()
                : categorias.Select(e => new CategoriaDTO(e.Id,e.Nome)).OrderBy(e => e.Nome).ToList();

            return Ok(categoriaDTOs);
        }
    }
}
