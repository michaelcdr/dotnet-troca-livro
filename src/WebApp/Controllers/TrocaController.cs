using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.HttpClients;
using TrocaLivro.Dominio.Responses;
using WebApp.Filtros;

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

            return View(model);
        }

        [HttpPost, AuthorizeCustomizado]
        public async Task<JsonResult> Solicitar(int disponibilizacaoTrocaId)
        {
            base.AtualizarToken(this.api);

            AppResponse<SolicitarTrocaResultado> resposta = await api.SolicitarTroca(new SolicitarTrocaCommand(disponibilizacaoTrocaId));
            return Json(resposta);
        }

        [AuthorizeCustomizado]
        public async Task<IActionResult> _LivrosDisponibilizadosParaTroca()
        {
            base.AtualizarToken(this.api);
            return PartialView();
        }

        [AuthorizeCustomizado]
        public async Task<IActionResult> _LivrosEnviados()
        {
            base.AtualizarToken(this.api);
            //AppResponse<SolicitarTrocaResultado> resposta = await api.ObterLivros
            return PartialView();
        }

        [AuthorizeCustomizado]
        public async Task<IActionResult> _TrocasSolicitadas()
        {
            base.AtualizarToken(this.api);
            return PartialView();
        }
    }
}