using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.HttpClients;
using TrocaLivro.Aplicacao.ViewModels;
using TrocaLivro.Dominio.Responses;
using WebApp.Filtros;

namespace WebApp.Controllers
{
    public class PacoteController : BaseController
    {
        private readonly PacoteApiClient api; 
        private readonly IMapper mapper;

        public PacoteController(PacoteApiClient httpClient,   IMapper mapper)
        {
            this.api = httpClient; 
            this.mapper = mapper;
        } 

        [AuthorizeCustomizado]
        public async Task<IActionResult> ConfirmarCompra(int id)
        {
            base.AtualizarToken(this.api);
            ObterPacoteResultado resultado = await api.ObterPacote(id);
            var viewModel = mapper.Map<ConfirmarCompraPacoteViewModel>(resultado);
            return View(viewModel);
        }

        [HttpPost, AuthorizeCustomizado]
        public async Task<IActionResult> ConfirmarCompra(ConfirmarCompraPacoteViewModel model)
        {
            base.AtualizarToken(this.api);
            AppResponse<ComprarPacoteResultado> resultado = await api.ComprarPacote(model.Id);

            if (!resultado.Sucesso)
            {
                ModelState.AddModelError("", resultado.Mensagem);
                return View(model);
            }
            return RedirectToAction("Index","Home");
        }
    }
}