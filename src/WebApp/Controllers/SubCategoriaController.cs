using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.HttpClients;
using TrocaLivro.Aplicacao.ViewModels;
using TrocaLivro.Dominio.Responses;
using WebApp.Filtros;

namespace WebApp.Controllers
{
    public class SubCategoriaController : Controller
    {
        private readonly SubCategoriaApiClient _apiSubCategorias;

        public SubCategoriaController(SubCategoriaApiClient api)
        {
            this._apiSubCategorias = api;
        }

        public IActionResult Cadastrar()
        {
            return PartialView(new CadastroSubCategoriaViewModel());
        }

        [HttpPost, ModelState, AuthorizeCustomizado]
        public async Task<IActionResult> Cadastrar(CriarSubCategoriaCommand model)
        {
            AppCommandResponse resposta = await _apiSubCategorias.Cadastrar(model);

            if (!resposta.Sucesso) return View(resposta);

            ObterSubCategoriasResultado resultado = await _apiSubCategorias.ObterSubCategorias();

            return Json(new { sucesso = true, subCategorias = resultado.SubCategorias });
        }
    }
}
