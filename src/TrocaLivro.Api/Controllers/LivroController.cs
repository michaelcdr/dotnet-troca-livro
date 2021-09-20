using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.CasosDeUsos.CadastrarLivro;
using TrocaLivro.Aplicacao.CasosDeUsos.DeletarLivro;
using TrocaLivro.Aplicacao.CasosDeUsos.EditarLivro;
using TrocaLivro.Dominio.DTO;
using TrocaLivro.Dominio.Requests;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Dominio.Services;

namespace TrocaLivro.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/livros")]
    public class LivroController : Controller
    {
        private IMediator _mediator;
        public LivroController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// Obtem uma lista de nivel. Quando nada for informado no termo de pesquisa todos livros são retornados.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get([FromQuery] ObterLivrosAdicionadosRecentementeQuery query)
        {
            AppResponse<ObterLivrosAdicionadosRecentementeResultado> resposta = await _mediator.Send(query);

            if (!resposta.Sucesso) return BadRequest(resposta.Erros);

            return Ok(resposta.Dados);
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> Dashboard([FromQuery] ObterDadosDashboardQuery query)
        {
            AppResponse<ObterDadosDashboardResultado> resposta = await _mediator.Send(query);

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
        [HttpPost, DisableRequestSizeLimit]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post([FromForm] CadastrarLivroCommand comando)
        {
            AppResponse<CadastrarLivroResultado> resultado = await _mediator.Send(comando);

            if (!resultado.Sucesso) return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpPut, DisableRequestSizeLimit]
        public async Task<IActionResult> Put([FromForm] EditarLivroCommand comando)
        {
            AppResponse<EditarLivroResultado> resultado = await _mediator.Send(comando);

            if (!resultado.Sucesso) return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpDelete("{livroId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int livroId)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            AppResponse<DeletarLivroResultado> resultado = await _mediator.Send(new DeletarLivroCommand(livroId, userId));

            if (!resultado.Sucesso) return BadRequest(resultado);
           
            return Ok(resultado);
        }
    }
}
