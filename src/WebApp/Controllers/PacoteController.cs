using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Dominio.Entidades;
using TrocaLivro.Dominio.Responses;
using TrocaLivro.Infra.Repositorios.Data;
using WebApp.Filtros;

namespace WebApp.Controllers
{
    public class PacoteController : Controller
    {
        private readonly PacotesRepositorio _pacotes;   
        private readonly HttpClient httpClient;

        public PacoteController(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this._pacotes = new PacotesRepositorio();  
        } 

        [AuthorizeCustomizado]
        public async Task<IActionResult> ConfirmarCompra(int id)
        {
            //PacotePontos pacote = _pacotes.Obter(id);
            return View(new ConfirmarCompraPacoteViewModel(id));
        }

        [HttpPost, AuthorizeCustomizado]
        public async Task<JsonResult> Comprar(int pacoteId)
        {
            var token = HttpContext.User.Claims.FirstOrDefault(e => e.Type == "Token").Value;
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var comando = new ComprarPacoteCommand(pacoteId);
            HttpResponseMessage resposta = await httpClient.PostAsJsonAsync($"Pacote/Comprar", comando);
             
            var conteudoResposta = await resposta.Content.ReadFromJsonAsync<AppResponse<AvaliarLivroResultado>>(); 
            return Json(resposta);
        }
    }
}