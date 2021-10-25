using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.HttpClients;
using TrocaLivro.Dominio.Responses;

namespace WebApp.Controllers
{
    public class TrocaController : Controller
    {
        private readonly LivroApiClient api;
        private readonly IMapper _mapper;
        public TrocaController(LivroApiClient livroApiClient, IMapper mapper)
        {
            this.api = livroApiClient;
            this._mapper = mapper;
        }

        private void AtualizarToken()
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
                this.api.AtualizarToken(HttpContext.User.Claims.FirstOrDefault(e => e.Type == "Token"));
        }

        public async Task<IActionResult> Solicitacao(int id)
        {
            AtualizarToken();

            AppResponse<ObterTrocaResultado> resposta = await api.ObterTroca(id);
            TrocarLivroViewModel model = _mapper.Map<TrocarLivroViewModel>(resposta.Dados);

            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> Solicitar(int disponibilizacaoTrocaId)
        {
            AppResponse<SolicitarTrocaResultado> resposta = await api.SolicitarTroca(new SolicitarTrocaCommand(disponibilizacaoTrocaId));
            return Json(resposta);
        }
    }
}