using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.CasosDeUsos.CadastrarLivro;
using TrocaLivro.Aplicacao.CasosDeUsos.EditarLivro;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Infra.Services;

namespace TrocaLivro.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/livros")]
    public class LivroController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IGerenciadorToken _tokenHandler;
        public LivroController(IMediator mediator, IGerenciadorToken tokenHandler)
        {
            this._mediator = mediator;
            this._tokenHandler = tokenHandler;
        }

        /// <summary>
        /// Obtem uma lista de nivel. Quando nada for informado no termo de pesquisa todos livros são retornados.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get([FromQuery] ObterLivrosQuery query)
        {
            AppResponse<ObterLivrosResultado> resposta = await _mediator.Send(query);

            if (!resposta.Sucesso) return BadRequest(resposta.Erros);

            return Ok(resposta.Dados.Livros);
        }

        [HttpGet("dashboard")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Dashboard()
        {
            AppResponse<ObterDadosDashboardResultado> resposta = await _mediator.Send(new ObterDadosDashboardQuery());

            if (!resposta.Sucesso) return BadRequest(resposta.Erros);

            return Ok(resposta.Dados);
        }

        /// <summary>
        /// Obtem uma lista de nivel. Quando nada for informado no termo de pesquisa todos livros são retornados.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("{livroId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get(int livroId)
        {
            AppResponse<ObterLivroResultado> resposta = await _mediator.Send(new ObterLivroQuery(livroId));

            if (!resposta.Sucesso) return BadRequest(resposta.Erros);

            return Ok(resposta.Dados);
        }

        /// <summary>
        /// Cadastra um novo livro.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, DisableRequestSizeLimit]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post([FromForm] CadastrarLivroCommand comando)
        {
            string token = HttpContext.Request.Headers["Authorization"];
            comando.Usuario = _tokenHandler.ObterNomeUsuario(token);

            AppResponse<CadastrarLivroResultado> resultado = await _mediator.Send(comando);

            if (!resultado.Sucesso) return BadRequest(resultado);

            return Ok(resultado);
        }

        /// <summary>
        /// Atualiza um livro atual
        /// </summary>
        /// <param name="comando"></param>
        /// <returns></returns>
        [HttpPut, DisableRequestSizeLimit]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Put([FromForm] EditarLivroCommand comando)
        {
            string token = HttpContext.Request.Headers["Authorization"];
            comando.Usuario = _tokenHandler.ObterNomeUsuario(token);

            AppResponse<EditarLivroResultado> resultado = await _mediator.Send(comando);

            if (!resultado.Sucesso) return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpDelete("{livroId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int livroId)
        {
            string token = HttpContext.Request.Headers["Authorization"];
            var comando = new DeletarLivroCommand(livroId, _tokenHandler.ObterNomeUsuario(token));
            
            AppResponse<DeletarLivroResultado> resultado = await _mediator.Send(comando);

            if (resultado.StatusCode == 404) return NotFound(resultado);

            if (!resultado.Sucesso) return BadRequest(resultado);
           
            return Ok(resultado);
        }

        [HttpPost("Avaliar")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Avaliar(AvaliarLivroCommand comando)
        {
            string token = HttpContext.Request.Headers["Authorization"];
            comando.Usuario = _tokenHandler.ObterNomeUsuario(token);

            AppResponse<AvaliarLivroResultado> resultado = await _mediator.Send(comando);
            if (!resultado.Sucesso) return BadRequest(resultado);

            return Ok(resultado);
        }
    }
}
