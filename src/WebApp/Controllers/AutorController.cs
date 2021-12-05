using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.DTO;
using TrocaLivro.Aplicacao.HttpClients;
using TrocaLivro.Aplicacao.ViewModels;
using TrocaLivro.Dominio.Responses;
using WebApp.Filtros;

namespace WebApp.Controllers
{
    public class AutorController :  BaseController
    {
        private readonly AutorApiClient _api; 

        public AutorController(AutorApiClient api)
        {
            this._api = api;
        }

        public IActionResult Cadastrar()
        {
            base.AtualizarToken(this._api);
            return PartialView(new CadastroAutorViewModel());
        }
        
        [HttpPost, ModelState, AuthorizeCustomizado]
        public async Task<IActionResult> Cadastrar([FromBody]CriarAutorCommand model)
        {
            base.AtualizarToken(this._api);
            AppCommandResponse resposta = await _api.CadastrarAutor(model);
            
            if (!resposta.Sucesso) return Json(resposta);

            List<AutorDTO> autores = await _api.ObterAutores();

            return Json(new { sucesso = true, autores });
        }
    }
}
