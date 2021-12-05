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
    public class SubCategoriaApiClient : IAtualizadorToken
    {
        private readonly HttpClient httpClient;
        private string admToken;
        private string token;
        public SubCategoriaApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public void AtualizarToken(Claim claimComToken)
        {
            this.token = claimComToken.Value;
        }

        public async Task<AppCommandResponse> Cadastrar(CriarSubCategoriaCommand comando)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.token);
            HttpResponseMessage resposta = await httpClient.PostAsJsonAsync($"SubCategorias", comando);
            var conteudoResposta = await resposta.Content.ReadFromJsonAsync<AppCommandResponse>();
            return conteudoResposta;
        }

        public async Task<List<SubCategoriaDTO>> ObterSubCategorias(int categoriaId)
        { 
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.token);
            HttpResponseMessage resposta = await httpClient.GetAsync($"SubCategorias/ObterTodasDaCategoria/{categoriaId}" );
            return await resposta.Content.ReadFromJsonAsync<List<SubCategoriaDTO>>();
        }
    }
}