using Microsoft.AspNetCore.Http;
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
    public class LivroController : Controller
    {
        private readonly ILivroService _livroService;

        public LivroController(ILivroService livroService)
        {
            this._livroService = livroService;
        }

        /// <summary>
        /// Obtem uma lista de nivel. Quando nada for informado no termo de pesquisa todos livros são retornados.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ObterTodosLivrosRequest request)
        {
            AppResponse<IList<LivroDTO>> resposta = await _livroService.ObterTodos(request);

            if (!resposta.Sucesso) return BadRequest(resposta.Erros);

            return Ok(resposta.Dados);
        }

        /// <summary>
        /// Obtem uma lista de nivel. Quando nada for informado no termo de pesquisa todos livros são retornados.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{livroId}")]
        public async Task<IActionResult> Get(int livroId)
        {
            AppResponse<LivroDTO> resposta = await _livroService.Obter(livroId);

            if (!resposta.Sucesso) return BadRequest(resposta.Erros);

            return Ok(resposta.Dados);
        }

        /// <summary>
        /// Cadastra um novo livro
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost,DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromForm] LivroRequest request)
        {
            AppResponse<LivroDTO> resposta = await _livroService.Criar(request);

            if (!resposta.Sucesso) return BadRequest(resposta);

            var uri = Url.Action("Get", new { id = resposta.Dados.Id });

            return Ok(resposta);
        }
    }
}
