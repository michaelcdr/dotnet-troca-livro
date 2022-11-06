using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.DTO;
using TrocaLivro.Aplicacao.HttpClients;
using TrocaLivro.Dominio.Responses;
using WebApp.Filtros;

namespace WebApp.Controllers
{
    public class CategoriaController :  BaseController
    {
        private readonly CategoriaApiClient _api;

        public CategoriaController(CategoriaApiClient api)
        {
            this._api = api;
        }

        [AuthorizeCustomizado]
        public IActionResult Cadastrar()
        {
            base.AtualizarToken(this._api);
            return PartialView(new CriarCategoriaViewModel());
        }

        [HttpPost, ModelStateValidador, AuthorizeCustomizado]
        public async Task<IActionResult> Cadastrar([FromBody] CriarCategoriaCommand model)
        {
            base.AtualizarToken(this._api);
            AppCommandResponse resposta = await _api.CadastrarCategoria(model);

            if (!resposta.Sucesso) return Json(resposta);

            List<CategoriaDTO> categorias = await _api.ObterCategorias();

            return Json(new { sucesso = true, categorias });
        }
    }
}