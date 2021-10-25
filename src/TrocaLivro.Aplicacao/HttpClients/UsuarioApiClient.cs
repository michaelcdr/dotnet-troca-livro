using AutoMapper;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.CasosDeUsos.LogarUsuario;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.HttpClients
{
    public class UsuarioApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public UsuarioApiClient(HttpClient httpClient, IMapper mapper)
        {
            this._httpClient = httpClient;
            this._mapper = mapper;
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
    }
}
