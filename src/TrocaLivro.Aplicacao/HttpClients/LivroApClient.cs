﻿using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using TrocaLivro.Aplicacao.CasosDeUsos;
using TrocaLivro.Aplicacao.CasosDeUsos.CadastrarLivro;
using TrocaLivro.Aplicacao.CasosDeUsos.EditarLivro;
using TrocaLivro.Aplicacao.DTO;
using TrocaLivro.Aplicacao.ViewModels;
using TrocaLivro.Dominio.Helpers;
using TrocaLivro.Dominio.Responses;

namespace TrocaLivro.Aplicacao.HttpClients
{
    public class LivroApClient
    {
        private readonly HttpClient httpClient;
        private readonly IMapper mapper;
        private const string APICONTROLLER_LIVRO = "livros";
        private const string APICONTROLLER_SUBCATEGORIA = "subcategorias";
        private string admToken;
        private string token;

        public void AtualizarToken(Claim claimComToken)
        {
            this.token = claimComToken.Value;
        }

        public LivroApClient(HttpClient httpClient, IMapper mapper, IMemoryCache memoryCache)
        {
            this.httpClient = httpClient;
            this.mapper = mapper; 
            this.admToken = memoryCache.Get("ADM_TOKEN").ToString();
        }

        public async Task<ObterDadosDashboardViewModel> ObterInformacoesHome()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", admToken);
            var resposta = await httpClient.GetAsync(string.Concat(APICONTROLLER_LIVRO, "/dashboard"));
            var dashboard = await resposta.Content.ReadFromJsonAsync<ObterDadosDashboardResultado>();
            ObterDadosDashboardViewModel indexViewModel = mapper.Map<ObterDadosDashboardViewModel>(dashboard);

            return indexViewModel;
        }

        public async Task<List<LivroCardModel>> ObterLivrosAdicionadosRecentemente()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", admToken);
            var resposta = await httpClient.GetAsync(APICONTROLLER_LIVRO);
            var livros = await resposta.Content.ReadFromJsonAsync<List<LivroCardModel>>();
            return livros;
        }

        private MultipartFormDataContent CriarFormDataContentParaCamposDoLivro(IEdicaoCadastroLivroCommand requisicao)
        {
            var content = new MultipartFormDataContent
            {
                { new StringContent(requisicao.Titulo), "\"titulo\"" },
                { new StringContent(requisicao.Subtitulo), BotarAspas("subtitulo") },
                { new StringContent(requisicao.Descricao), BotarAspas("descricao") },
                { new StringContent(requisicao.ISBN), BotarAspas("isbn") },
                { new StringContent(requisicao.SubCategoriaId.ToString()), BotarAspas("subCategoriaId") }
            };

            foreach (var item in requisicao.AutorId)
                content.Add(new StringContent(item.ToString()), BotarAspas("autorId"));

            content.Add(new StringContent(requisicao.EditoraId.ToString()), BotarAspas("editoraId"));
            content.Add(new StringContent(requisicao.Ano.ToString()), BotarAspas("ano"));
            content.Add(new StringContent(requisicao.NumeroPaginas.ToString()), BotarAspas("numeroPaginas"));
            return content;
        }

        private HttpContent CriarMultipartFormDataParaEdicaoLivro(EditarLivroCommand requisicao)
        {
            var content = CriarFormDataContentParaCamposDoLivro(requisicao);
            content.Add(new StringContent(requisicao.Id.ToString()), "\"id\"");
            content.Add(new StringContent(requisicao.ImagensAtuaisId ?? string.Empty), "\"imagensAtuaisId\"");
            foreach (var imagem in requisicao.Imagens)
            {
                var imagemBytes = new ByteArrayContent(FileHelper.ConvertToBytes(imagem));
                imagemBytes.Headers.Add("content-type", "image/jpg");
                content.Add(imagemBytes, BotarAspas("imagens"), BotarAspas("imagem-atual.jpg"));
            }

            return content;
        } 

        private HttpContent CriarMultipartFormDataParaLivro(CadastrarLivroCommand requisicao)
        {
            MultipartFormDataContent content = CriarFormDataContentParaCamposDoLivro(requisicao);

            foreach (var imagem in requisicao.Imagens)
            {
                var imagemBytes = new ByteArrayContent(FileHelper.ConvertToBytes(imagem));
                imagemBytes.Headers.Add("content-type", "image/jpg");
                content.Add(imagemBytes, BotarAspas("imagens"), BotarAspas("imagem-atual.jpg"));
            }

            return content;
        }

        public async Task<AppResponse<EditarLivroResultado>> EditarLivro(EditarLivroViewModel request)
        {
            var comando = mapper.Map<EditarLivroCommand>(request);
            HttpContent content = CriarMultipartFormDataParaEdicaoLivro(comando);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.token);
            HttpResponseMessage resposta = await httpClient.PutAsync(APICONTROLLER_LIVRO, content);

            var conteudoResposta = await resposta.Content.ReadFromJsonAsync<AppResponse<EditarLivroResultado>>();
            return conteudoResposta;
        }

        public async Task<AppResponse<CadastrarLivroResultado>> CadastrarLivro(CadastrarLivroViewModel request)
        {
            CadastrarLivroCommand comando = mapper.Map<CadastrarLivroCommand>(request); 
            HttpContent content = CriarMultipartFormDataParaLivro(comando);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.token);
            HttpResponseMessage resposta = await httpClient.PostAsync(APICONTROLLER_LIVRO, content);

            var conteudoResposta = await resposta.Content.ReadFromJsonAsync<AppResponse<CadastrarLivroResultado>>();
            return conteudoResposta;
        }

        public async Task<List<LivroDTO>> ObterListaLivros(string termoPesquisa)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", admToken);

            var resposta = await httpClient.GetAsync($"${APICONTROLLER_LIVRO}?TermoPesquisa={termoPesquisa}");
            var livros = await resposta.Content.ReadFromJsonAsync<List<LivroDTO>>();
            return livros;
        }

        public async Task<AppResponse<DeletarLivroResultado>> DeletarLivro(int livroId)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.token);
            HttpResponseMessage resposta = await httpClient.DeleteAsync($"{APICONTROLLER_LIVRO}/{livroId}");

            return await resposta.Content.ReadFromJsonAsync<AppResponse<DeletarLivroResultado>>();
        }

        public async Task<List<EditoraDTO>> ObterEditoras()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", admToken);

            HttpResponseMessage resposta = await httpClient.GetAsync("Editora");
            resposta.EnsureSuccessStatusCode();
            var editoraDTOs = await resposta.Content.ReadFromJsonAsync<List<EditoraDTO>>();

            return editoraDTOs;
        }

        public async Task<LivroDTO> ObterLivro(int livroId)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", admToken);

            HttpResponseMessage resposta = await httpClient.GetAsync($"{APICONTROLLER_LIVRO}/{livroId}");

            ObterLivroResultado resultado = await resposta.Content.ReadFromJsonAsync<ObterLivroResultado>();

            return resultado.Livro;
        }

        public async Task<List<AutorDTO>> ObterAutores()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.admToken);

            HttpResponseMessage resposta = await httpClient.GetAsync("Autor");
            var dados = await resposta.Content.ReadFromJsonAsync<List<AutorDTO>>();
            return dados;
        }

        public async Task<List<CategoriaDTO>> ObterCategorias()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.admToken);

            HttpResponseMessage resposta = await httpClient.GetAsync("Categoria");
            var dados = await resposta.Content.ReadFromJsonAsync<List<CategoriaDTO>>();
            return dados;
        }

        public async Task<List<SubCategoriaDTO>> ObterSubCategorias(int categoriaId)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.admToken);

            HttpResponseMessage resposta = await httpClient.GetAsync($"{APICONTROLLER_SUBCATEGORIA}/ObterTodasDaCategoria/{categoriaId}");
            var dados = await resposta.Content.ReadFromJsonAsync<List<SubCategoriaDTO>>();
            return dados;
        }

        private string BotarAspas(string valor) => $"\"{valor}\"";        
    }
}