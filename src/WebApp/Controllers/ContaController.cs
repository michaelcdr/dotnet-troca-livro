using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using WebApp.HttpClients;

namespace WebApp.Controllers
{
    public class ContaController : Controller
    {
        private readonly ContaApiClient contaApi;
        
        public ContaController(ContaApiClient contaApiClient)
        {
            this.contaApi = contaApiClient;
        }

        public IActionResult Index() => View(new ContaModel());

        public IActionResult _Logar() => View(new LoginModel()); 

        [HttpPost]
        public async Task<IActionResult> Logar(LoginModel model)
        {
            string token = await contaApi.GerarToken(model);

            var resultado = await contaApi.Logar(model);
            return View();
        }

        public IActionResult _Registrar() => View(new RegistrarModel());

        [HttpPost]
        public async Task<JsonResult> Registrar(RegistrarModel model)
        {
            var resultado =  await contaApi.Registrar(model);           


            return Json(new { });
        }

        public IActionResult AprovarTrocas()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Logar()
        {
            return RedirectToAction("Login");
        }
    }
}
