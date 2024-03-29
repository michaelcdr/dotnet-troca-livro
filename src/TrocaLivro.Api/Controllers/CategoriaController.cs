﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.DTO;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Dominio.Transacoes;

namespace TrocaLivro.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0"), ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/v{version:apiVersion}/categorias")]
    public class CategoriaController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediator _mediator;

        public CategoriaController(IMediator mediator, IUnitOfWork uow)
        {
            this._mediator = mediator;
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

        [HttpGet("ComSubCategorias")]
        public async Task<IActionResult> ObterComSubCategorias()
        {
            AppResponse<ObterCategoriasResultado> resposta = await _mediator.Send(new ObterCategoriasQuery());

            if (!resposta.Sucesso) return BadRequest(resposta);

            return Ok(resposta);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriarCategoriaCommand command)
        {
            AppCommandResponse resposta = await _mediator.Send(command);

            if (!resposta.Sucesso) return BadRequest(resposta);

            var categoria = await _mediator.Send(new ObterCategoriaPorNomeQuery(command.Nome));

            var uri = Url.Action("Get", new { id = categoria.Dados.Id });

            return Created(uri, resposta);
        }
    }
}