﻿using System.Collections.Generic;
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
    public class AutorApiClient : IAtualizadorToken
    {
        private readonly HttpClient httpClient;
        private string admToken;
        private string token;
        public AutorApiClient(HttpClient httpClient )
        {
            this.httpClient = httpClient; 
        }
        public void AtualizarToken(Claim claimComToken)
        {
            this.token = claimComToken.Value;
        }

        public async Task<AppCommandResponse> CadastrarAutor(CriarAutorCommand comando)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.token);
            HttpResponseMessage resposta = await httpClient.PostAsJsonAsync($"Autores", comando);
            var conteudoResposta = await resposta.Content.ReadFromJsonAsync<AppCommandResponse>();
            return conteudoResposta;
        }

        public async Task<List<AutorDTO>> ObterAutores()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.admToken);
            HttpResponseMessage resposta = await httpClient.GetAsync("Autores");
            var dados = await resposta.Content.ReadFromJsonAsync<List<AutorDTO>>();
            return dados;
        }
    }
}