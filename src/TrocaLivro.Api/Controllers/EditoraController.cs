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
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EditoraController : Controller
    {
        private IEditoraService _editoraService;

        public EditoraController(IEditoraService editoraService)
        {
            this._editoraService = editoraService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            AppResponse<IList<EditoraDTO>> resposta = await _editoraService.ObterTodas();

            if (!resposta.Sucesso) return BadRequest(resposta.Erros);

            return Ok(resposta.Dados);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EditoraRequest request)
        {
            AppResponse<Editora> resposta = await _editoraService.Criar(request);

            if (!resposta.Sucesso) return BadRequest(resposta);

            var uri = Url.Action("Get", new { id = resposta.Dados.Id });

            return Created(uri, resposta);
        }
    }
}
