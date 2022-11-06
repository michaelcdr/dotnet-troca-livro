using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.HttpClients;
using TrocaLivro.Dominio.Responses;
using WebApp.Filtros;

namespace WebApp.Controllers
{
    public class SubCategoriaController : BaseController
    {
        private readonly SubCategoriaApiClient _apiSubCategorias;

        public SubCategoriaController(SubCategoriaApiClient api)
        {
            this._apiSubCategorias = api;
        }

        [AuthorizeCustomizado]
        public IActionResult Cadastrar(int categoriaId)
        {
            return PartialView(new CriarSubCategoriaViewModel() { CategoriaId = categoriaId });
        }

        [HttpPost, ModelStateValidador, AuthorizeCustomizado]
        public async Task<IActionResult> Cadastrar([FromBody]CriarSubCategoriaCommand model)
        {
            base.AtualizarToken(this._apiSubCategorias);

            AppCommandResponse resposta = await _apiSubCategorias.Cadastrar(model);

            if (!resposta.Sucesso) return Json(resposta);

            var subCategorias = await _apiSubCategorias.ObterSubCategorias(model.CategoriaId);

            return Json(new { sucesso = true, subCategorias = subCategorias });
        }
    } 
}
