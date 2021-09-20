using AutoMapper;
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
using TrocaLivro.Dominio.DTO;
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
        private string admToken = string.Empty;
        private string token = string.Empty;

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
            var resposta = await httpClient.GetAsync(string.Concat(APICONTROLLER_LIVRO,"/dashboard"));
            var dashboard = await resposta.Content.ReadFromJsonAsync<ObterDadosDashboardResultado>();
            ObterDadosDashboardViewModel indexViewModel = mapper.Map<ObterDadosDashboardViewModel>(dashboard);

            return indexViewModel;
        }

        public async Task<List<LivroDTO>> ObterLivrosAdicionadosRecentemente()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", admToken);
            var resposta = await httpClient.GetAsync(APICONTROLLER_LIVRO);
            var livros = await resposta.Content.ReadFromJsonAsync<List<LivroDTO>>();
            return livros;
        }

        private HttpContent CriarMultipartFormDataParaLivro(CadastrarLivroCommand requisicao)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(requisicao.Titulo), "\"titulo\"");
            content.Add(new StringContent(requisicao.Subtitulo), BotarAspas("subtitulo"));
            content.Add(new StringContent(requisicao.Descricao), BotarAspas("descricao"));
            content.Add(new StringContent(requisicao.ISBN), BotarAspas("isbn"));
            content.Add(new StringContent(requisicao.SubCategoriaId.ToString()), BotarAspas("subCategoriaId"));

            foreach (var item in requisicao.AutorId)
                content.Add(new StringContent(item.ToString()), BotarAspas("autorId"));

            content.Add(new StringContent(requisicao.EditoraId.ToString()), BotarAspas("editoraId"));
            content.Add(new StringContent(requisicao.Ano.ToString()), BotarAspas("ano"));
            content.Add(new StringContent(requisicao.NumeroPaginas.ToString()), BotarAspas("numeroPaginas"));

            foreach (var imagem in requisicao.Imagens)
            {
                var imagemBytes = new ByteArrayContent(FileHelper.ConvertToBytes(imagem));
                imagemBytes.Headers.Add("content-type", "image/jpg");
                content.Add(imagemBytes, BotarAspas("imagens"), BotarAspas("imagem-atual.jpg"));
            }

            return content;
        }

        public Task<AppResponse<EditarLivroResultado>> EditarLivro(EditarLivroViewModel model)
        {
            throw new System.NotImplementedException();
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
            var resposta = await httpClient.GetAsync($"${APICONTROLLER_LIVRO}?TermoPesquisa={termoPesquisa}");
            var livros = await resposta.Content.ReadFromJsonAsync<List<LivroDTO>>();
            return livros;
        }

        public async Task<List<EditoraDTO>> ObterEditoras()
        {
            HttpResponseMessage resposta = await httpClient.GetAsync("Editora");
            resposta.EnsureSuccessStatusCode();

            var dados = await resposta.Content.ReadFromJsonAsync<List<EditoraDTO>>();
            return dados;
        }

        public async Task<LivroDTO> ObterLivro(int livroId)
        {
            HttpResponseMessage resposta = await httpClient.GetAsync($"{APICONTROLLER_LIVRO}/{livroId}");
            LivroDTO livros = await resposta.Content.ReadFromJsonAsync<LivroDTO>();
            return livros;
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