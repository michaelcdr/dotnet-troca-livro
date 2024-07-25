using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.HttpClients;
using TrocaLivro.Dominio.Entidades;
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

        public PartialViewResult _PacotesPontos(List<PacotePontos> pacotes)
        {
            return PartialView(pacotes);
        }

        public IActionResult Privacy() 
        {
            throw new System.Exception("aaaaaaaaa");
        }

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var model = new ErrorViewModel();

            if(id == 500)
            {
                model.Mensagem = "Ocorreu um erro tente novamente mais tarde.";
                model.Titulo = "Ops, ocorreu um erro!";
                model.ErrorCode= 500;
            }
            else if(id == 404)
            {
                model.Mensagem = "A página que você solicitou não existe.";
                model.Titulo = "Ops, página não encontrada!";
                model.ErrorCode = 500;
            }
            else if (id == 403)
            {
                model.Mensagem = "Você não tem permissão para estar aqui.";
                model.Titulo = "Acesso negado!";
                model.ErrorCode = 500;
                
            }
            else
            {
                return StatusCode(404);
            }
            return View(model);
        }

        public async Task GerarManutencao()
        {
            await _api.GerarUrlAmigaveisSubCategorias();
        }
    }
}