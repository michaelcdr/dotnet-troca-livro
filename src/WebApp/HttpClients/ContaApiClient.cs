using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Dominio.Responses;

namespace WebApp.HttpClients
{
    public class ContaApiClient
    {
        private readonly HttpClient httpClient;

        public ContaApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> Logar(LoginModel model)
        {
            //HttpContent content = new HttpContent();

            //var resposta = await httpClient.PostAsync("Logar", model);
            
            return string.Empty;
        }

        public async Task<string> GerarToken(LoginModel model)
        {
            //var resposta = await httpClient.PostAsync("Logar", model);
            return string.Empty;
        }

        public async Task<AppResponse<RegistrarResultado>> Registrar(RegistrarModel model)
        {
            var resposta = await httpClient.PostAsJsonAsync("Conta/Registrar", model);
            resposta.EnsureSuccessStatusCode();

            AppResponse<RegistrarResultado> appResponse = await resposta.Content.ReadFromJsonAsync<AppResponse<RegistrarResultado>>();

            return appResponse;
        }
    }
}
