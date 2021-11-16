using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.HttpClients;
using TrocaLivro.Aplicacao.ViewModels;
using TrocaLivro.Dominio.Responses;
using WebApp.Filtros;
using WebApp.Helpers;

namespace WebApp.Controllers
{
    public class TrocaController : BaseController
    {
        private readonly LivroApiClient api;
        private readonly IMapper _mapper;
        public TrocaController(LivroApiClient livroApiClient, IMapper mapper)
        {
            this.api = livroApiClient;
            this._mapper = mapper;
        }

        public async Task<IActionResult> Solicitacao(int id)
        {
            base.AtualizarToken(this.api);

            AppResponse<ObterTrocaResultado> resposta = await api.ObterTroca(id);
            TrocarLivroViewModel model = _mapper.Map<TrocarLivroViewModel>(resposta.Dados);
            model.Estados = EstadosHelper.ObterTodos();
            return View(model);
        }

        [HttpPost, AuthorizeCustomizado, ModelState]
        public async Task<JsonResult> Solicitar(TrocarLivroViewModel model)
        {
            base.AtualizarToken(this.api);
            var comando = _mapper.Map<SolicitarTrocaCommand>(model);
            AppResponse<SolicitarTrocaResultado> resposta = await api.SolicitarTroca(comando);
            return Json(resposta);
        }

        [HttpPost, AuthorizeCustomizado]
        public async Task<JsonResult> Aprovar(int trocaId)
        {
            base.AtualizarToken(this.api);
            AppResponse<AprovarTrocaResultado> resposta = await api.AprovarTroca(trocaId);
            return Json(resposta);
        }

        [HttpPost, AuthorizeCustomizado]
        public async Task<JsonResult> MarcarComoEnviada(int id)
        {
            base.AtualizarToken(this.api);
            AppCommandResponse resposta = await api.MarcarLivroComoEnviado(id);
            return Json(resposta);
        }

        [AuthorizeCustomizado]
        public async Task<IActionResult> _LivrosDisponibilizadosParaTroca()
        {
            base.AtualizarToken(this.api);
            AppResponse<ObterTrocasDisponibilizadasPorUsuarioResultado> resposta = await api.ObterTrocasDisponibilizadasPorUsuario();
            return PartialView(resposta.Dados.Livros);
        }

        [AuthorizeCustomizado]
        public async Task<IActionResult> _LivrosEnviados()
        {
            return PartialView();
        }

        [AuthorizeCustomizado]
        public async Task<IActionResult> _TrocasSolicitadasAoUsuarioLogado()
        {
            base.AtualizarToken(this.api);
            AppResponse<ObterTrocasSolicitadasAoUsuarioLogadoResultado> resposta = await api.ObterTrocasSolicitadasAoUsuarioLogado();
            List<TrocaSolicitadaViewModel> solicitacoes = resposta.Dados.Solicitacoes;
            return PartialView(solicitacoes);
        }

        [AuthorizeCustomizado]
        public async Task<IActionResult> _Detalhes(int id)
        {
            base.AtualizarToken(this.api);
            AppResponse<ObterTrocaResultado> resposta = await api.ObterTroca(id);
            var model = _mapper.Map<TrocaViewModel>(resposta.Dados);
            return PartialView(model);
        }

        [AuthorizeCustomizado]
        public async Task<IActionResult> _TrocasSolicitadasPeloUsuarioLogado()
        {
            base.AtualizarToken(this.api);
            AppResponse<ObterTrocasSolicitadasPeloUsuarioLogadoResultado> resposta = await api.ObterTrocasSolicitadasPeloUsuarioLogado();
            List<TrocaSolicitadaViewModel> solicitacoes = resposta.Dados.Solicitacoes;
            return PartialView(solicitacoes);
            
        }

        [AuthorizeCustomizado]
        public async Task<IActionResult> _Aprovadas()
        {
            return PartialView();
        }
    }
}