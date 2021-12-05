using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.DTO;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.HttpClients
{
    public class EditoraApiClient : IAtualizadorToken
    {
        private readonly HttpClient httpClient;
        private string admToken;
        private string token;

        public EditoraApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public void AtualizarToken(Claim claimComToken)
        {
            this.token = claimComToken.Value;
        }

        public async Task<AppCommandResponse> CadastrarEditora(CriarEditoraCommand comando)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.token);
            HttpResponseMessage resposta = await httpClient.PostAsJsonAsync("Editoras", comando);
            return await resposta.Content.ReadFromJsonAsync<AppCommandResponse>();
        }

        public async Task<List<EditoraDTO>> ObterEditoras()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.admToken);
            HttpResponseMessage resposta = await httpClient.GetAsync("Editoras");
            return await resposta.Content.ReadFromJsonAsync<List<EditoraDTO>>();
        }
    }
}