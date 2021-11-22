using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.Helpers;
using TrocaLivro.Aplicacao.ViewModels;
using TrocaLivro.Dominio.Responses;

namespace WebApp.ViewComponents
{
    public class CategoriasViewComponent :ViewComponent
    { 
        private readonly HttpContext _httpContext;
        private readonly HttpClient _httpClient;
        private readonly string _token;
        public CategoriasViewComponent(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.BaseAddress = new Uri(AppHelper.ObterApiPath());

            this._httpClient = httpClient;  
            this._httpContext = httpContextAccessor.HttpContext;

            if (_httpContext.User.Identity != null && _httpContext.User.Identity.IsAuthenticated)
                this._token = _httpContext.User.Claims.FirstOrDefault(e => e.Type == "Token").Value;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this._token);
            HttpResponseMessage resposta = await _httpClient.GetAsync("Categoria/ComSubCategorias"); 
            var appResponse = await resposta.Content.ReadFromJsonAsync<AppResponse<ObterCategoriasResultado>>();

            return View(new CategoriasMenuViewModel(appResponse.Dados.Categorias));
        }
    }
}
