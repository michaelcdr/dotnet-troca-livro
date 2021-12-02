using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.HttpClients;
using TrocaLivro.Aplicacao.Services;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Infra.Repositorios.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly LivroApiClient _api; 

        public HomeController(LivroApiClient api )
        {
            this._api = api; 
        }

        public async Task<IActionResult> Index()
        {
            ObterDadosDashboardViewModel model = await _api.ObterInformacoesHome();
            return View(model);
        }

        public async Task<PartialViewResult> _PacotesPontos(List<PacotePontos> pacotes)
        {
            return PartialView(pacotes);
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task GerarManutencao()
        {
            await _api.GerarUrlAmigaveisSubCategorias();
        }
    }
}