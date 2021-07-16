using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrocaLivro.Dominio.DTO;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Requests;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Dominio.Services;

namespace TrocaLivro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : Controller
    {
        private IAutorService _autorService;
        public AutorController(IAutorService autorService)
        {
            this._autorService = autorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            AppResponse<IList<AutorDTO>> resposta = await _autorService.ObterTodas();


            if (!resposta.Sucesso) return BadRequest(resposta.Erros);

            return Ok(resposta.Dados);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AutorRequest request)
        {
            AppResponse<Autor> resposta = await _autorService.Criar(request); 

            if (!resposta.Sucesso) return BadRequest(resposta);

            var uri = Url.Action("Get", new { id = resposta.Dados.Id });

            return Created(uri, resposta);
        }
    }
}
