using AutoMapper;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.CasosDeUsos.LogarUsuario;
using TrocaLivro.Dominio.Helpers;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.HttpClients
{
    public class UsuarioApiClient : IAtualizadorToken
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private string token;
        public UsuarioApiClient(HttpClient httpClient, IMapper mapper)
        {
            this._httpClient = httpClient;
            this._mapper = mapper;
        }

        public void AtualizarToken(Claim claimComToken)
        {
            this.token = claimComToken.Value;
        }

        public async Task<int> ObterPontos()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.token);
            HttpResponseMessage resposta = await _httpClient.GetAsync("Usuario/Pontos");
            var appResponse = await resposta.Content.ReadFromJsonAsync<AppResponse<ObterPontosResultado>>();

            return appResponse.Dados.Pontos;
        }

        public async Task<AppResponse<ComprarPacoteResultado>> ComprarPacote(int pacoteId)
        {
            var command = new ComprarPacoteCommand(pacoteId);
            HttpResponseMessage resposta = await _httpClient.PostAsJsonAsync("Usuario/Logar", command);
            var appResponse = await resposta.Content.ReadFromJsonAsync<AppResponse<ComprarPacoteResultado>>();
            return appResponse;
        }

        public async Task<AppResponse<LogarUsuarioResultado>> Logar(LogarUsuarioModel model)
        {
            HttpResponseMessage resposta = await _httpClient.PostAsJsonAsync("Usuario/Logar", model);
            var appResponse = await resposta.Content.ReadFromJsonAsync<AppResponse<LogarUsuarioResultado>>();
            return appResponse;
        }
        
        public async Task<AppResponse<RegistrarUsuarioResultado>> Registrar(RegistrarUsuarioModel model)
        {
            var commando = _mapper.Map<RegistrarUsuarioCommand>(model);
            var resposta = await _httpClient.PostAsJsonAsync("Usuario/Registrar", commando);
            var appResponse = await resposta.Content.ReadFromJsonAsync<AppResponse<RegistrarUsuarioResultado>>();
            return appResponse;
        }

        public async Task<AppResponse<ObterUsuarioResultado>> Obter(string userName)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.token);
            HttpResponseMessage resposta = await _httpClient.GetAsync($"Usuario/{userName}");
            var appResponse = await resposta.Content.ReadFromJsonAsync<AppResponse<ObterUsuarioResultado>>();
            return appResponse;
        }

        private string BotarAspas(string valor) => $"\"{valor}\"";

        public async Task<AppCommandResponse> Atualizar(EditarUsuarioCommand model)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.token);

            var content = new MultipartFormDataContent
            {
                { new StringContent(model.UsuarioId), "\"usuarioId\"" },
                { new StringContent(model.Nome), "\"nome\"" },
                { new StringContent(model.Sobrenome), "\"sobrenome\"" },
                { new StringContent(model.Email), "\"email\"" }
            };

            if (model.Avatar != null && model.Avatar.Length > 0)
            {
                var imagemBytes = new ByteArrayContent(FileHelper.ConvertToBytes(model.Avatar));
                imagemBytes.Headers.Add("content-type", "image/jpg");
                content.Add(imagemBytes, BotarAspas("avatar"), BotarAspas("imagem-atual.jpg"));
            }

            HttpResponseMessage resposta = await _httpClient.PostAsync($"Usuario", content);
            var appResponse = await resposta.Content.ReadFromJsonAsync<AppCommandResponse>();
            return appResponse;
        }
    }
}
