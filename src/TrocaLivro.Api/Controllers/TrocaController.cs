﻿using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Infra.Services;

namespace TrocaLivro.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/trocas")]
    public class TrocaController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IGerenciadorToken _gerenciadorToken;

        public TrocaController(IMediator mediator, IGerenciadorToken gerenciadorToken)
        {
            this._mediator = mediator;
            this._gerenciadorToken = gerenciadorToken;
        }

        /// <summary>
        /// Disponibiliza um livro para troca.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("Disponibilizar"), DisableRequestSizeLimit, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Disponibilizar([FromForm]DisponibilizarLivroParaTrocaCommand comando)
        {
            string token = HttpContext.Request.Headers["Authorization"];
            comando.Usuario = _gerenciadorToken.ObterNomeUsuario(token);
            AppResponse<DisponibilizarLivroParaTrocaResultado> resposta = await _mediator.Send(comando);

            if (!resposta.Sucesso) return BadRequest(resposta.Erros);

            return Ok(resposta);
        }

        [HttpPost("Solicitar"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Solicitar(SolicitarTrocaCommand comando)
        {
            string token = HttpContext.Request.Headers["Authorization"];
            comando.Usuario = _gerenciadorToken.ObterNomeUsuario(token);
            AppResponse<SolicitarTrocaResultado> resposta = await _mediator.Send(comando);

            if (!resposta.Sucesso) return BadRequest(resposta);

            return Ok(resposta);
        }

        [HttpPost("Aprovar"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Aprovar(AprovarTrocaCommand comando)
        {
            string token = HttpContext.Request.Headers["Authorization"];
            comando.Usuario = _gerenciadorToken.ObterNomeUsuario(token);
            AppResponse<AprovarTrocaResultado> resposta = await _mediator.Send(comando);

            if (!resposta.Sucesso) return BadRequest(resposta);

            return Ok(resposta);
        }

        [HttpPost("MarcarLivroComoEnviado"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> MarcarLivroComoEnviado(MarcarLivroComoEnviadoCommand comando)
        {
            string token = HttpContext.Request.Headers["Authorization"];
            comando.Usuario = _gerenciadorToken.ObterNomeUsuario(token);
            AppCommandResponse resposta = await _mediator.Send(comando);
            if (!resposta.Sucesso) return BadRequest(resposta);
            return Ok(resposta);
        }

        [HttpPost("MarcarLivroComoRecebido"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> MarcarLivroComoRecebido(MarcarLivroComoRecebidoCommand comando)
        {
            AppCommandResponse resposta = await _mediator.Send(comando);
            if (!resposta.Sucesso) return BadRequest(resposta);
            return Ok(resposta);
        }

        [HttpGet("{id}"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get(int id)
        {
            string token = HttpContext.Request.Headers["Authorization"];
            string usuario = _gerenciadorToken.ObterNomeUsuario(token);
            AppResponse<ObterTrocaResultado> resposta = await _mediator.Send(new ObterTrocaQuery { Id = id, Usuario = usuario });
            return Ok(resposta);
        }

        [HttpGet("SolicitadasAoUsuarioLogado"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> SolicitadasAoUsuarioLogado()
        {
            string token = HttpContext.Request.Headers["Authorization"];
            string usuario = _gerenciadorToken.ObterNomeUsuario(token);
            AppResponse<ObterTrocasSolicitadasAoUsuarioLogadoResultado> resposta = await _mediator.Send(new ObterTrocasSolicitadasAoUsuarioLogadoQuery(usuario));
            return Ok(resposta);
        }
        
        [HttpGet("SolicitadasPeloUsuarioLogado"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> SolicitadasPeloUsuarioLogado()
        {
            string token = HttpContext.Request.Headers["Authorization"];
            string usuario = _gerenciadorToken.ObterNomeUsuario(token);
            AppResponse<ObterTrocasSolicitadasPeloUsuarioLogadoResultado> resposta = await _mediator.Send(new ObterTrocasSolicitadasPeloUsuarioLogadoQuery(usuario));
            return Ok(resposta);
        }

        [HttpGet("Disponibilizadas"), Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Disponibilizadas()
        {
            string token = HttpContext.Request.Headers["Authorization"];
            string usuario = _gerenciadorToken.ObterNomeUsuario(token);
            
            AppResponse<ObterTrocasDisponibilizadasPorUsuarioResultado> resposta = await _mediator.Send(
                new ObterTrocasDisponibilizadasPorUsuarioQuery(usuario)
            );
            return Ok(resposta);
        }
    }
}