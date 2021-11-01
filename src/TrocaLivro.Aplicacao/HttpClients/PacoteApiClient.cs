using AutoMapper;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.HttpClients
{
    public class PacoteApiClient: IAtualizadorToken
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private string token;
        public PacoteApiClient(HttpClient httpClient, IMapper mapper)
        {
            this._httpClient = httpClient;
            this._mapper = mapper;
        }

        public void AtualizarToken(Claim claimComToken)
        {
            this.token = claimComToken.Value;
        } 

        public async Task<AppResponse<ComprarPacoteResultado>> ComprarPacote(int pacoteId)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.token);
            var comando = new ComprarPacoteCommand(pacoteId);
            HttpResponseMessage resposta = await _httpClient.PostAsJsonAsync("pacotes/Comprar", comando);
            var appResponse = await resposta.Content.ReadFromJsonAsync<AppResponse<ComprarPacoteResultado>>();
            return appResponse;
        }

        public async Task<ObterPacoteResultado> ObterPacote(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.token);
            HttpResponseMessage resposta = await _httpClient.GetAsync($"pacotes/Obter/{id}");
            var resultado = await resposta.Content.ReadFromJsonAsync<ObterPacoteResultado>();
            return resultado;
        }
    }
}
